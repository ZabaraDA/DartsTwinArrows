using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifeCycleManager : MonoBehaviour, IProjectileLifeCycleManager
{
    private List<IProjectilePresenter> _activePresenters;

    private void Awake()
    {
        _activePresenters = new List<IProjectilePresenter>();
    }

    private void Update()
    {
        // Обновляем все активные презентеры
        for (int i = _activePresenters.Count - 1; i >= 0; i--)
        {
            _activePresenters[i].Update(Time.deltaTime);
        }
    }

    public void RegisterPresenter(IProjectilePresenter presenter)
    {
        Debug.Log("ProjectileLifeCycleManager");
        if (!_activePresenters.Contains(presenter))
        {
            _activePresenters.Add(presenter);
        }
    }

    public void UnregisterPresenter(IProjectilePresenter presenter)
    {
        _activePresenters.Remove(presenter);
    }

    // Для очистки при завершении сцены/игры
    void OnDestroy()
    {
        for (int i = _activePresenters.Count - 1; i >= 0; i--) // Идем в обратном порядке для безопасного удаления
        {
            _activePresenters[i].Dispose();
        }
        _activePresenters.Clear();
    }
}
