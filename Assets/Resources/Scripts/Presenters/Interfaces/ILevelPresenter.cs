using System;
using UnityEngine;

public interface ILevelPresenter : IInitializable, IDisposable
{
    event Action<ILevelPresenter> OnPresenterLevelCompletedTriggered;
}
