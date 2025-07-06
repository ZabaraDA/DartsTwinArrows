using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private IGamePresenter _gamePresenter;
    private void Start()
    {
        _gamePresenter.Initialize();
    }

    [Inject]
    public void Inject(IGamePresenter gamePresenter)
    {
        _gamePresenter = gamePresenter;
    }

}
