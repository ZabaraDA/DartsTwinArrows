using System;
using System.Collections;
using UnityEngine;

public class WeaponView : MonoBehaviour, IWeaponView
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Transform _projectileSpawnPosition;

    public event Action OnViewMouseButtonClick;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnViewMouseButtonClick?.Invoke();
        }
    }

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
    public void StartWeaponCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
