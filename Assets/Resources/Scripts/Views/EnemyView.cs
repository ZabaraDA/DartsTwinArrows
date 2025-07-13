using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyView : MonoBehaviour, IEnemyView
{
    public event Action<Collider2D> OnViewCollider2DTriggered;
    public event Action<int> OnViewTakeDamageTriggered;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private GameObject _textContainer;
    [SerializeField]
    private TMP_Text _text;

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
        OnViewCollider2DTriggered?.Invoke(other);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("ENEMY TakeDamage");
        OnViewTakeDamageTriggered?.Invoke(damage);
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void OpenCard()
    {
        _spriteRenderer.sprite = null;
        _textContainer.SetActive(true);
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
