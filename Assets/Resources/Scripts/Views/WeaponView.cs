using UnityEngine;

public class WeaponView : MonoBehaviour, IWeaponView
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    public void SetRotation(Quaternion rotation)
    {
        gameObject.transform.rotation = rotation;
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
