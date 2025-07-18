using System;
using System.Collections.Generic;
using System.Linq;
using AppsFlyerSDK;
using UnityEngine;

public class AppsflyerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _devKey;

    private string _appsflyerId;
    private string _attributionData;
    private bool _isOrganic = true;
    private DataStorage _dataStorage;
    private UrlBuilder _urlBuilder;

    public string AppsflyerId => _appsflyerId;
    public bool IsOrganic => _isOrganic;
    public string AttributionData => _attributionData;
    public bool InitializationComplete { get; private set; }

    [SerializeField] private CheckUrl _checkUrl;

    private void Awake()
    {
        if (_checkUrl.CheckUrlSO == 1)
        {
            this.enabled = false;
            return;
        }

        _dataStorage = GetComponent<DataStorage>();
        _urlBuilder = GetComponent<UrlBuilder>();
    }

    public void Initialize()
    {
        if (string.IsNullOrEmpty(_devKey))
        {
            Debug.LogError("[AppsFlyer] DevKey is not set!");
            return;
        }

        InitializationComplete = false;
        AppsFlyer.initSDK(_devKey, Application.identifier);
        AppsFlyer.startSDK();
        _appsflyerId = AppsFlyer.getAppsFlyerId();
        AppsFlyer.getConversionData(gameObject.name);
    }

    private void onConversionDataReceived(string conversionData)
    {
        Debug.Log($"[AppsFlyer] Raw conversion data: {conversionData}");

        try
        {
            var data = AppsFlyer.CallbackStringToDictionary(conversionData);
            Debug.Log("[AppsFlyer] Parsed conversion data: " + DictionaryToString(data));

            if (data.TryGetValue("af_status", out object afStatusObj))
            {
                string status = afStatusObj.ToString();
                _isOrganic = status == "Organic";
                Debug.Log($"[AppsFlyer] Install type: {status}");

                if (!_isOrganic && data.TryGetValue("campaign", out object campaignObj))
                {
                    _attributionData = campaignObj.ToString();
                    Debug.Log($"[AppsFlyer] Campaign data: {_attributionData}");

                    // Строим URL на основе данных из хранилища
                    var (baseUrl, companyKey) = _dataStorage.LoadBaseConfig();
                    string packageName = Application.identifier;
                    string url = _urlBuilder.BuildNonOrganicUrl(_attributionData, "", _appsflyerId, packageName);

                    // Добавляем все параметры из conversionData для логов
                    foreach (var key in data.Keys)
                    {
                        if (key != "campaign" && key != "af_status")
                            _attributionData += $"\n{key}: {data[key]}";
                    }

                    if (data.TryGetValue("media_source", out object mediaSource))
                        Debug.Log($"[AppsFlyer] Media Source: {mediaSource}");

                    if (data.TryGetValue("install_time", out object installTime))
                        Debug.Log($"[AppsFlyer] Install Time: {installTime}");
                }
                else
                {
                    // Органическая установка
                    var (baseUrl, companyKey) = _dataStorage.LoadBaseConfig();
                    string packageName = Application.identifier;
                    string url = _urlBuilder.BuildOrganicUrl("", _appsflyerId, packageName);
                }
            }

            InitializationComplete = true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[AppsFlyer] Conversion data processing failed: {e}");
        }
    }


    private string ParseCampaignData(string campaignData)
    {
        if (!campaignData.StartsWith("link."))
            return string.Empty;

        // Удаляем "link." и разбиваем по *
        string[] parts = campaignData.Substring(5).Split('*');
        if (parts.Length < 2)
            return string.Empty;

        string companyKey = parts[0]; // Pfrcw1
        string queryParams = parts[1]
            .Replace(":", "=")
            .Replace("|", "&");

        return $"{companyKey}?{queryParams}";
    }

    // Автоматически вызывается при ошибке конверсионных данных
    private void onConversionDataRequestFailure(string error)
    {
        Debug.LogError("AppsFlyer Conversion Error: " + error);
    }

    // Автоматически вызывается при получении deep link (название метода должно быть точным!)
    private void onDeepLinking(string deepLinkData)
    {
        var deepLinkArgs = new DeepLinkEventsArgs(deepLinkData);
        Debug.Log("Deep Link Received: " + deepLinkArgs.deepLink);
    }

    private string DictionaryToString(Dictionary<string, object> dict)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var kvp in dict)
        {
            sb.AppendLine(kvp.Key + ": " + kvp.Value);
        }
        return sb.ToString();
    }

    // В AppsFlyer 6.17.1 отписываться не нужно – метод onDeepLinking вызывается автоматически
    private void OnDestroy() { }
}