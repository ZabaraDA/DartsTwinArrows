using UnityEngine;

public interface IEnemyFactory : IFactory<IEnemyPresenter, IEnemyModel>
{
    IEnemyPresenter Create(int id, IEnemyTypeModel projectileType, Vector2 position, string text = null);
}
