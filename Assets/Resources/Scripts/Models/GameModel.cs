using System;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : IGameModel
{
    public ICollection<IWeaponTypeModel> WeaponTypeModels { get; set; }
    public ICollection<ILevelModel> LevelModels { get; set; }

    public GameModel(ICollection<IWeaponTypeModel> weaponTypeModels, ICollection<ILevelModel> levelModels) 
    {
        WeaponTypeModels = weaponTypeModels;
        LevelModels = levelModels;
    }
}
