using UnityEngine;

public interface IProjectileFactory : IFactory<IProjectilePresenter, IProjectileModel>
{
    IProjectilePresenter Create(int id, string name, Vector2 startPosition, Vector2 direction, IProjectileTypeModel projectileType);
}
