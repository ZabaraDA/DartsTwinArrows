using System;
using System.Collections.Generic;

public interface IGameModel
{
    ICollection<ILevelModel> LevelModels { get; set; }  
    ILevelModel CurrentLevelModel { get; set; }
    
    event Action<ILevelModel> OnModelCurrentLevelModelChanged;
}
