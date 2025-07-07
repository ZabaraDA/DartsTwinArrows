using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GamePresenter : IGamePresenter
{
    private IGameView _view;
    private IGameModel _model;
    private IStatisticsPresenter _statisticsPresenter;
    private ILevelFactory _levelFactory;
    public GamePresenter(IGameView view, IGameModel model, IStatisticsPresenter statisticsPresenter, ILevelFactory levelFactory)
    {
        _view = view;
        _model = model;
        _statisticsPresenter = statisticsPresenter;
        _levelFactory = levelFactory;
    }
    public void Dispose()
    {
        _view.OnViewPauseButtonClicked -= HandleOnViewPauseButtonClicked;
        _view.OnViewContinueButtonClicked -= HandleOnViewContinueButtonClicked;
    }
    public void Initialize()
    {
        _view.OnViewPauseButtonClicked += HandleOnViewPauseButtonClicked;
        _view.OnViewContinueButtonClicked += HandleOnViewContinueButtonClicked;
        _statisticsPresenter.Initialize();
        Debug.Log("GamePresenter inizialized");
    }

    private void HandleOnViewPauseButtonClicked()
    {
        Time.timeScale = 0f;
        _view.SetActivePausePanel(true);
    }
    private void HandleOnViewContinueButtonClicked()
    {
        _view.SetActivePausePanel(false);
        Time.timeScale = 1.0f;
    }
}
