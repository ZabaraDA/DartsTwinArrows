using System;
using UnityEngine;

public interface IEnemyView
{
    void SetPosition(Vector2 newPosition);
    void SetText(string text);
    void SetRotation(Quaternion newRotation);

    GameObject GetGameObject();

    event Action<Collider2D> OnViewCollider2DTriggered;
    event Action<int> OnViewTakeDamageTriggered;
    void TakeDamage(int damage);
    void SetSprite(Sprite sprite);

    void OpenCard();
}
