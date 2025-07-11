using UnityEngine;

public interface IProjectileFactory : IFactory<IProjectilePresenter, IProjectileModel>
{
    IProjectilePresenter Create(int id, Transform parent, Vector2 position, IProjectileTypeModel projectileType);
}
