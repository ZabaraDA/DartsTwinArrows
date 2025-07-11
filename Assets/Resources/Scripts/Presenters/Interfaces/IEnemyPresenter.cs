using System;
using UnityEngine;

public interface IEnemyPresenter : IInitializable, IDisposable, IUpdatable<float>
{
    event Action<IEnemyModel> OnPresenterEnemyModelDestoyed;
    event Action<IEnemyPresenter> OnPresenterEnemyPresenterDestoyed;
}
