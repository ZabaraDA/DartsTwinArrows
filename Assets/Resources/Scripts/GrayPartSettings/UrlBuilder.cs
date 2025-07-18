using System;
using UnityEngine;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using static System.Net.WebRequestMethods;

public class UrlBuilder : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private string _defaultBaseUrl = "http://track.twindrts.site/";
    [SerializeField] private string _defaultCompanyKey = "JiLMQMEjefOhiJRC2Uxr";

    private string _currentBaseUrl;
    private string _currentCompanyKey;
    private bool _firebaseInitialized = false;
    private DataStorage _dataStorage;

    [SerializeField] private CheckUrl _checkUrl;

    private void Awake()
    {
        if (_checkUrl.CheckUrlSO == 1)
        {
            this.enabled = false;
            return;
        }

        _dataStorage = gameObject.AddComponent<DataStorage>();
        InitializeFirebase();
    }

    private void Start()
    {
        UpdateConfig(_defaultBaseUrl, _defaultCompanyKey);
    }

    private void InitializeFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError($"Failed to initialize Firebase: {task.Exception}");
                InitializeWithDefaults();
                return;
            }

            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                SetupRemoteConfig();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                InitializeWithDefaults();
            }
        });
    }

    private void SetupRemoteConfig()
    {
        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;

        // Устанавливаем дефолтные значения
        var defaults = new System.Collections.Generic.Dictionary<string, object>();
        defaults.Add("base_url", _defaultBaseUrl);
        defaults.Add("company_key", _defaultCompanyKey);

        remoteConfig.SetDefaultsAsync(defaults).ContinueWithOnMainThread(task =>
        {
            // Настройка времени кэширования (12 часов)
            var fetchInterval = TimeSpan.FromHours(12).TotalSeconds;
            remoteConfig.FetchAsync(TimeSpan.FromSeconds(fetchInterval)).ContinueWithOnMainThread(fetchTask =>
            {
                if (fetchTask.IsCanceled)
                {
                    Debug.LogWarning("RemoteConfig fetch canceled.");
                }
                else if (fetchTask.IsFaulted)
                {
                    Debug.LogWarning("RemoteConfig fetch encountered an error.");
                }
                else if (fetchTask.IsCompleted)
                {
                    Debug.Log("RemoteConfig fetch completed successfully!");
                }

                // Активируем полученные значения
                remoteConfig.ActivateAsync().ContinueWithOnMainThread(activateTask =>
                {
                    UpdateConfigFromRemote();
                    _firebaseInitialized = true;
                });
            });
        });
    }

    private void UpdateConfigFromRemote()
    {
        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;

        _currentBaseUrl = remoteConfig.GetValue("base_url").StringValue;
        _currentCompanyKey = remoteConfig.GetValue("company_key").StringValue;

        Debug.Log($"RemoteConfig values loaded: BaseUrl={_currentBaseUrl}, CompanyKey={_currentCompanyKey}");

        // Сохраняем базовые данные в хранилище
        _dataStorage.SaveBaseConfig(_currentBaseUrl, _currentCompanyKey);
    }

    private void InitializeWithDefaults()
    {
        _currentBaseUrl = _defaultBaseUrl;
        _currentCompanyKey = _defaultCompanyKey;
        Debug.LogWarning("Using default URL config values");

        // Сохраняем базовые данные в хранилище
        _dataStorage.SaveBaseConfig(_currentBaseUrl, _currentCompanyKey);
    }

    public string BuildOrganicUrl(string oneSignalId, string appsflyerId, string packageName)
    {
#if UNITY_EDITOR
        // Тестовые значения для редактора
        if (string.IsNullOrEmpty(oneSignalId)) oneSignalId = "test_onesignal_id";
        if (string.IsNullOrEmpty(appsflyerId)) appsflyerId = "test_appsflyer_id";
#endif

        string url = $"{_currentBaseUrl}/{_currentCompanyKey}?onesignal_id={oneSignalId}&appsflyer_id={appsflyerId}&package_name={packageName}";
        _dataStorage.SaveUrl(url, true);
        return url;
    }

    public string BuildNonOrganicUrl(string campaignData, string oneSignalId, string appsflyerId, string packageName)
    {
        if (string.IsNullOrEmpty(campaignData) || !campaignData.StartsWith("link."))
            return BuildOrganicUrl(oneSignalId, appsflyerId, packageName);

        try
        {
            string processed = campaignData.Substring(5);
            string[] parts = processed.Split('*');

            string companyKey = parts[0];
            string queryParams = parts.Length > 1 ? parts[1] : "";

            queryParams = queryParams
                .Replace(":", "=")
                .Replace("|", "&");

            string url = $"{_currentBaseUrl}/{companyKey}?{queryParams}&onesignal_id={oneSignalId}&appsflyer_id={appsflyerId}&package_name={packageName}";
            _dataStorage.SaveUrl(url, false);
            return url;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to parse campaign data: {e.Message}");
            return BuildOrganicUrl(oneSignalId, appsflyerId, packageName);
        }
    }

    // Метод для ручного обновления конфигов
    public void UpdateConfig(string baseUrl, string companyKey)
    {
        _currentBaseUrl = string.IsNullOrEmpty(baseUrl) ? _defaultBaseUrl : baseUrl;
        _currentCompanyKey = string.IsNullOrEmpty(companyKey) ? _defaultCompanyKey : companyKey;

        Debug.Log($"Updated URL config: BaseUrl={_currentBaseUrl}, CompanyKey={_currentCompanyKey}");
        _dataStorage.SaveBaseConfig(_currentBaseUrl, _currentCompanyKey);
    }
}