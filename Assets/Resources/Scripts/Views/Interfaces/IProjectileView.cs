using System;
using UnityEngine;

public interface IProjectileView
{
    void SetPosition(Vector2 newPosition);
    void SetRotation(Quaternion newRotation);
    void SetSprite(Sprite sprite);
    void DetachFromParent();

    GameObject GetGameObject();

    event Action<Collider2D> OnViewCollider2DTriggered;
}
