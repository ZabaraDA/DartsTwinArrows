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
    private Transform _weaponSpawnPosition;
    [SerializeField]
    private Transform _enemySpawnPosition;
}