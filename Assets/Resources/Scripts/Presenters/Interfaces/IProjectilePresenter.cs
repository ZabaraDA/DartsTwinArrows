using System;
using UnityEngine;

public interface IProjectilePresenter : IInitializable, IDisposable, IUpdatable<float>
{
    void StartMoving();

    event Action<IProjectilePresenter> OnPresenterDestroyProjectileTriggered;
}
