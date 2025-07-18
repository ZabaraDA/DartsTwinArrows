using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyLifeCycleManager : MonoBehaviour, IEnemyLifeCycleManager
{
    private List<IEnemyPresenter> _activePresenters;

    private static EnemyLifeCycleManager _instance;

    private void Awake()
    {
        // ���������� �������� Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject); // �� ���������� ��� �������� ����� �����
        _activePresenters = new List<IEnemyPresenter>();
    }

    private void Update()
    {
        // ��������� ��� �������� ����������
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

    // �������� ���� ����� �����
    public void ClearAllPresenters()
    {
        // ���������� ToList() ����� �������� ������ ��� ��������� ��������� �� ����� ��������
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

    // ��� ������� ��� ���������� �����/����
    void OnDestroy()
    {
        for (int i = _activePresenters.Count - 1; i >= 0; i--) // ���� � �������� ������� ��� ����������� ��������
        {
            _activePresenters[i].Dispose();
        }
        _activePresenters.Clear();
    }
}
