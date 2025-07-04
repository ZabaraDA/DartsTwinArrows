using System;
using UnityEngine;

public class ProjectileView : MonoBehaviour, IProjectileView
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    public event Action<Collider2D> OnViewCollider2DTriggered;

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public void SetRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnViewCollider2DTriggered");
        OnViewCollider2DTriggered?.Invoke(other);
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
