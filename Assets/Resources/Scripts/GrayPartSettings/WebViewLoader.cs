using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WebViewLoader : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _timeout = 6f;
    [SerializeField] private GameObject _webViewContainer;
    [SerializeField] private bool _forceOrganicInEditor = true;

    [Header("Сервисы")]
    [SerializeField] private AppsflyerManager _appsflyer;
    [SerializeField] private OneSignalManager _oneSignal;
    [SerializeField] private UrlBuilder _urlBuilder;
    [SerializeField] private DataStorage _dataStorage;

    private WebViewObject _webView;
    private bool _isWebViewOpen = false;
    private string _currentUrl;
    private bool _isOrganic;

    [SerializeField] private CheckUrl _checkUrl;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        if (_dataStorage == null) _dataStorage = GetComponent<DataStorage>();
        if (_urlBuilder == null) _urlBuilder = GetComponent<UrlBuilder>();
        if (_appsflyer == null) _appsflyer = GetComponent<AppsflyerManager>();
        if (_oneSignal == null) _oneSignal = GetComponent<OneSignalManager>();
    }

    private void Start()
    {
        if (_checkUrl.CheckUrlSO == 1)
        {
            LoadGame();
            return;
        }

        StartCoroutine(InitializeAndLoad());
    }

    private IEnumerator InitializeAndLoad()
    {
        // Инициализация сервисов
        _appsflyer.Initialize();
        _oneSignal.Initialize();

        float timeout = 6f;
        float startTime = Time.time;
        bool servicesReady = false;

        while (Time.time - startTime < timeout && !servicesReady)
        {
            servicesReady = _appsflyer.InitializationComplete &&
                          (!string.IsNullOrEmpty(_oneSignal.OneSignalId) || Application.isEditor);

            // Добавляем лог для отладки
            if (!servicesReady)
            {
                Debug.Log($"Ожидание инициализации сервисов... " +
                         $"Appsflyer: {_appsflyer.InitializationComplete}, " +
                         $"OneSignal: {!string.IsNullOrEmpty(_oneSignal.OneSignalId)}");
            }

            yield return new WaitForSeconds(0.5f); // Проверяем каждые 0.5 секунды
        }

        if (!servicesReady)
        {
            Debug.LogWarning("Не все сервисы инициализировались за отведенное время. " +
                           $"Appsflyer: {_appsflyer.InitializationComplete}, " +
                           $"OneSignal ID: {_oneSignal.OneSignalId}");

            // Продолжаем работу с дефолтными значениями
            _isOrganic = true;
        }

        // Проверка сохраненных данных
        string savedUrl = _dataStorage.LoadUrl();
        bool savedOrganicStatus = _dataStorage.LoadOrganicStatus();

        if (!string.IsNullOrEmpty(savedUrl))
        {
            _currentUrl = savedUrl;
            _isOrganic = savedOrganicStatus;
            StartCoroutine(CheckAndShowWebView());
            yield break;
        }

        // Построение URL
        string oneSignalId = Application.isEditor ? "editor_test_id" : _oneSignal.OneSignalId;
        string appsflyerId = Application.isEditor ? "editor_test_afid" : _appsflyer.AppsflyerId;

        _currentUrl = _isOrganic ?
            _urlBuilder.BuildOrganicUrl(oneSignalId, appsflyerId, Application.identifier) :
            _urlBuilder.BuildNonOrganicUrl(_appsflyer.AttributionData, oneSignalId, appsflyerId, Application.identifier);

        _dataStorage.SaveUrl(_currentUrl, _isOrganic);
        StartCoroutine(CheckAndShowWebView());
    }

    private IEnumerator CheckAndShowWebView()
    {
#if UNITY_EDITOR
        _currentUrl = AddTestParameters(_currentUrl);
        Debug.Log($"Режим редактора. URL: {_currentUrl}");
        ShowWebView();
        yield break;
#else
    // Для HTTP-ссылок пропускаем проверку доступности
    if (_currentUrl.StartsWith("http://"))
    {
        ShowWebView();
        yield break;
    }

    // Для HTTPS проверяем доступность
    bool urlAvailable = false;
    using (UnityWebRequest request = UnityWebRequest.Head(_currentUrl))
    {
        request.timeout = (int)_timeout;
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            ShowWebView();
        }
        else 
        {
            Debug.LogWarning($"URL недоступен: {request.error}");
            // Устанавливаем флаг ошибки
            _checkUrl.CheckUrlSO = 1;
            LoadGame();
        }
    }
#endif
    }

    private string AddTestParameters(string url)
    {
        if (string.IsNullOrEmpty(url)) return url;

        string testParams = "editor_test=true&device=unity_editor";
        return url.Contains("?") ? $"{url}&{testParams}" : $"{url}?{testParams}";
    }

    private void ShowWebView()
    {
        Debug.Log($"Opening URL: {_currentUrl}");

#if UNITY_EDITOR
        Application.OpenURL(_currentUrl);
        LoadGame();
#elif UNITY_ANDROID || UNITY_IOS
    PrepareForWebView();

    if (_webView == null)
    {
        _webView = (new GameObject("WebView")).AddComponent<WebViewObject>();
    }

    // Упрощенная инициализация
    _webView.Init(
        cb: msg => {
            Debug.Log($"WebView callback: {msg}");
            if(msg == "close") CloseWebView();
        },
        err: error => {
            Debug.LogError($"WebView error: {error}");
            // Попытка открыть в браузере при ошибке
            Application.OpenURL(_currentUrl);
            CloseWebView();
        }
    );

    // Принудительная загрузка HTTP
    _webView.LoadURL(_currentUrl);
    _webView.SetVisibility(true);
    _webView.SetMargins(0, 0, 0, 0); 
    _isWebViewOpen = true;

    _checkUrl.CheckUrlSO = 1;
#else
    LoadGame();
#endif
    }

    private void PrepareForWebView()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void HandleWebViewCallback(string message)
    {
        Debug.Log($"WebView callback: {message}");

        if (message == "close" || message.Contains("error") || message.Contains("success"))
        {
            CloseWebView();
            LoadGame();
        }
    }

    private void HandleWebViewError(string error)
    {
        Debug.LogError($"Ошибка WebView: {error}");
        CloseWebView();
        LoadGame();
    }

    private void LoadGame()
    {
        Debug.Log("Загрузка основной игры");
        CloseWebView();
    }

    private void CloseWebView()
    {
        if (!_isWebViewOpen) return;

        Time.timeScale = 1;
        AudioListener.pause = false;
        Screen.sleepTimeout = SleepTimeout.SystemSetting;

        if (_webView != null)
        {
            _webView.SetVisibility(false);
            Destroy(_webView.gameObject);
            _webView = null;
        }

        if (_webViewContainer != null)
        {
            _webViewContainer.SetActive(false);
        }

        _isWebViewOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isWebViewOpen)
            {
                if (_webView != null && _webView.CanGoBack())
                {
                    _webView.GoBack();
                }
            }
            else
            {
                Application.Quit();
            }
        }
    }

    private void OnDestroy()
    {
        CloseWebView();
    }
}