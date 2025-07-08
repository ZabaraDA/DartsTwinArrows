using UnityEngine;

public interface IWeaponFactory : IFactory<IWeaponPresenter, IWeaponModel>
{
    IWeaponPresenter Create(int id, Vector2 startPosition, IWeaponTypeModel type);
    
}
