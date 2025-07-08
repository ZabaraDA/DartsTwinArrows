using System.Collections.Generic;
using UnityEngine;

public class WeaponLifeCycleManager : MonoBehaviour, IWeaponLifeCycleManager
{
    private List<IWeaponPresenter> _activePresenters;

    private void Awake()
    {
        _activePresenters = new List<IWeaponPresenter>();
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        // ��������� ��� �������� ����������
        for (int i = _activePresenters.Count - 1; i >= 0; i--)
        {
            _activePresenters[i].Update(mousePosition);
        }
    }

    public void RegisterPresenter(IWeaponPresenter presenter)
    {
        if (!_activePresenters.Contains(presenter))
        {
            _activePresenters.Add(presenter);
        }
    }

    public void UnregisterPresenter(IWeaponPresenter presenter)
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
