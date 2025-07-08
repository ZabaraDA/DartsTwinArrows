using System;
using System.Numerics;
using UnityEngine;

public interface IGameView
{
    event Action OnViewPauseButtonClicked;
    event Action OnViewContinueButtonClicked;
    void SetActivePausePanel(bool isActive);
    void SetLevelText(string text);
    void SetScoreText(string text);

    Transform GetWeaponSpawnPosition();
}

