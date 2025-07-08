using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameView : MonoBehaviour, IGameView
{
    [Header("UI Elements")]
    [SerializeField] 
    private TMP_Text _scoreText;
    [SerializeField] 
    private TMP_Text _levelText;
    [SerializeField] 
    private Button _pauseButton;
    [SerializeField] 
    private Button _continueButton;
    [SerializeField] 
    private GameObject _pausePanel;
    [SerializeField] 
    private GameObject _menuPanel;

    [SerializeField]
    private Transform _weaponSpawnPosition;
    [SerializeField]
    private Transform _enemySpawnPosition;

    public event Action OnViewPauseButtonClicked;
    public event Action OnViewContinueButtonClicked;

    private void Start()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnPauseButtonClicked()
    {
        OnViewPauseButtonClicked?.Invoke();
    }
    private void OnContinueButtonClicked()
    {
        OnViewContinueButtonClicked?.Invoke();
    }

    public void SetActivePausePanel(bool isActive)
    {
        _pausePanel.SetActive(isActive);
        _menuPanel.SetActive(!isActive);
    }

    public void SetLevelText(string text)
    {
        _levelText.text = text;
    }

    public void SetScoreText(string text)
    {
        _scoreText.text = text;
    }

    public Transform GetWeaponSpawnPosition()
    {
        return _weaponSpawnPosition;
    }
}