using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModel : IGameModel
{
    public static int CurrentLevelNumber;
    public ICollection<ILevelModel> LevelModels { get; set; }

    private ILevelModel _currentLevelModel; 
    public ILevelModel CurrentLevelModel 
    { 
        get
        {
            return _currentLevelModel;
        }
        set 
        {
            if (_currentLevelModel != value)
            {
                _currentLevelModel = value;
                CurrentLevelNumber = _currentLevelModel.Number;
                OnModelCurrentLevelModelChanged?.Invoke(_currentLevelModel);
                Debug.Log($"Field '{nameof(CurrentLevelModel)}' changed in class {GetType()}. Value: {CurrentLevelModel.Number}");
            }
        }
    }
    public event Action<ILevelModel> OnModelCurrentLevelModelChanged;

    public GameModel(ICollection<ILevelModel> levelModels) 
    {
        LevelModels = levelModels;

        CurrentLevelModel = levelModels.FirstOrDefault(x => x.Number == 1);
        Debug.Log("Levels Count: " + levelModels.Count);
    }

}
