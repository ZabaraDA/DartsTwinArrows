using UnityEngine;

public interface IWeaponFactory : IFactory<IWeaponPresenter, IWeaponModel>
{
    IWeaponPresenter Create(int id, string name, Sprite sprite, Vector2 startPosition, Vector2 direction, IProjectileTypeModel projectileType);
    
}
