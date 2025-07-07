using System;
using System.Collections.Generic;

public interface IGameModel
{
    ICollection<IWeaponTypeModel> WeaponTypeModels { get; set; }
    ICollection<ILevelModel> LevelModels { get; set; }  
}
