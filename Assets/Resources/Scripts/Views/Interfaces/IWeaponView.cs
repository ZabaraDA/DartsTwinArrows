using System;
using System.Collections;
using UnityEngine;

public interface IWeaponView
{
    public event Action OnViewMouseButtonClick;
    void SetSprite(Sprite sprite);
    void SetRotation(Quaternion rotation);
    Transform GetProjectileSpawnPosition();
    void StartWeaponCoroutine(IEnumerator coroutine);
}
