using System.Collections;
using UnityEngine;

public interface IWeaponPresenter : IInitializable, IDisposable, IUpdatable<Vector2>
{
    IEnumerator PrepareProjectile();
    void LaunchProjectile();
}
