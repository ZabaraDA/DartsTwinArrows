using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyLifeCycleManager : MonoBehaviour, IEnemyLifeCycleManager
{
    private List<IEnemyPresenter> _activePresenters;

    private static EnemyLifeCycleManager _instance;

    private void Awake()
    {
        // Реализация паттерна Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject); // Не уничтожать при загрузке новой сцены
        _activePresenters = new List<IEnemyPresenter>();
    }

    private void Update()
    {
        // Обновляем все активные презентеры
        for (int i = _activePresenters.Count - 1; i >= 0; i--)
        {
            _activePresenters[i].Update(Time.deltaTime);
        }
    }

    public void RegisterPresenter(IEnemyPresenter presenter)
    {
        if (!_activePresenters.Contains(presenter))
        {
            _activePresenters.Add(presenter);
        }
    }

    // Добавить этот новый метод
    public void ClearAllPresenters()
    {
        // Используем ToList() чтобы избежать ошибок при изменении коллекции во время итерации
        foreach (var presenter in _activePresenters.ToList())
        {
            presenter.Dispose();
        }
        _activePresenters.Clear();
    }

    public void UnregisterPresenter(IEnemyPresenter presenter)
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
