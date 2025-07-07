using UnityEngine;

public interface IWeaponView
{
    void SetSprite(Sprite sprite);
    void SetRotation(Quaternion rotation);
    Transform GetProjectileSpawnPosition();
}
