using UnityEngine;

public interface IProjectileFactory
{
    IProjectilePresenter CreateProjectile(int id, Vector2 startPosition, Vector2 direction, IProjectileTypeModel projectileType);
    IProjectilePresenter CreateProjectile(IProjectileModel projectileModel);
}
