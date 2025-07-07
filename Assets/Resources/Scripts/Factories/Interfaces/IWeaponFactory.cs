using UnityEngine;

public interface IWeaponFactory : IFactory<IWeaponPresenter, IWeaponModel>
{
    IWeaponPresenter Create(int id, string name, Vector2 startPosition, Vector2 direction, IWeaponTypeModel type);
    
}
