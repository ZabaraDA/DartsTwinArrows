using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private IGamePresenter _gamePresenter;
    private IStatisticsPresenter _statisticsPresenter;
    private void Start()
    {
        Time.timeScale = 1.0f;
        _gamePresenter.Initialize();
        _statisticsPresenter.Initialize();
    }

    [Inject]
    public void Inject(IGamePresenter gamePresenter, IStatisticsPresenter statisticsPresenter)
    {
        _gamePresenter = gamePresenter;
        _statisticsPresenter = statisticsPresenter;
    }

    private void OnDestroy()
    {
        _gamePresenter.Dispose();
        _statisticsPresenter.Dispose();
    }
}
