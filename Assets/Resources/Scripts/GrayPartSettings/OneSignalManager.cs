using UnityEngine;
using OneSignalSDK;
using System.Collections;
using OneSignalSDK.Debug.Models;

public class OneSignalManager : MonoBehaviour
{
    [SerializeField] private string _appId;

    private string _oneSignalId;
    private bool _initialized = false;

    public string OneSignalId => _oneSignalId;
    public bool Initialized => _initialized;

    [SerializeField] private CheckUrl _checkUrl;
    public void Initialize()
    {
        if (_checkUrl.CheckUrlSO == 1) return;

        if (_initialized) return;

        // 1. Инициализация OneSignal
        OneSignal.Initialize(_appId);

        // 2. Настройка логов (используем enum вместо int)
        OneSignal.Debug.LogLevel = LogLevel.Verbose;

        // 3. Подписка на события (правильный формат для версии 5.1.14)
        OneSignal.Notifications.Clicked += (sender, args) => {
            Debug.Log($"[OneSignal] Notification clicked:\n" +
                     $"Title: {args.Notification.Title}\n" +
                     $"Body: {args.Notification.Body}");
        };

        // 4. Запуск корутины для получения ID
        StartCoroutine(InitializeCoroutine());
    }

    private IEnumerator InitializeCoroutine()
    {



        // Ждем инициализации User
        while (OneSignal.User.PushSubscription == null || string.IsNullOrEmpty(OneSignal.User.PushSubscription.Id))
        {
            yield return new WaitForSeconds(0.5f);
        }

        // Получаем OneSignal ID
        _oneSignalId = OneSignal.User.PushSubscription.Id;
        Debug.Log($"[OneSignal] Initialized. User ID: {_oneSignalId}");

        // Запрашиваем разрешение на уведомления
        if (!PlayerPrefs.HasKey("onesignal_permission_requested"))
        {
            OneSignal.Notifications.RequestPermissionAsync(false);
            PlayerPrefs.SetInt("onesignal_permission_requested", 1);
            PlayerPrefs.Save();
        }

        _initialized = true;
    }

    private void OnDestroy()
    {
        // В этой версии явная отписка не требуется
    }
}