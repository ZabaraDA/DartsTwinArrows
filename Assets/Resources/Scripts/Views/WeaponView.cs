using UnityEngine;

public class WeaponView : MonoBehaviour, IWeaponView
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Transform _projectileSpawnPosition;

    public Transform GetProjectileSpawnPosition()
    {
        return _projectileSpawnPosition.transform;
    }

    public void SetRotation(Quaternion rotation)
    {
        gameObject.transform.rotation = rotation;
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
