using System;
using UnityEngine;

public interface IStatisticsPresenter : IInitializable, IDisposable, IUpdatable<float>
{
    void ChangeVisibilityStatisticsPanel(bool isVisible);
}
