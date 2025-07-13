using System.Linq;
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

        _model.OnModelCurrentLevelModelChanged -= HandleOnModelCurrentLevelModelChanged;
        //_statisticsPresenter.Dispose();
    }
    public void Initialize()
    {
        _view.OnViewPauseButtonClicked += HandleOnViewPauseButtonClicked;
        _view.OnViewContinueButtonClicked += HandleOnViewContinueButtonClicked;
        _model.OnModelCurrentLevelModelChanged += HandleOnModelCurrentLevelModelChanged;
        //_statisticsPresenter.Initialize();

        LoadLevel(_model.CurrentLevelModel);
        Debug.Log("GamePresenter inizialized");
    }

    private void HandleOnModelCurrentLevelModelChanged(ILevelModel levelModel)
    {
        LoadLevel(levelModel);
    }
    private void LoadLevel(ILevelModel levelModel)
    {
        ILevelPresenter levelPresenter = _levelFactory.Create(levelModel);
        levelPresenter.OnPresenterLevelCompletedTriggered += HandleOnPresenterLevelCompletedTriggered;

        _view.SetLevelText($"LEVEL {levelModel.Number}");
    }

    private void LoadNextLevel(ILevelModel levelModel)
    {
        Debug.Log("LOADNEXTLEVEL");
        ILevelModel nextLevelModel = _model.LevelModels.FirstOrDefault(x => x.Number == (levelModel.Number + 1));
        if (nextLevelModel != null)
        {
            _model.CurrentLevelModel = nextLevelModel;
        }
        else
        {
            _statisticsPresenter.StopTimer();
            _statisticsPresenter.ChangeVisibilityStatisticsPanel(true);
        }
    }

    private void HandleOnPresenterLevelCompletedTriggered(ILevelPresenter levelPresenter, ILevelModel levelModel)
    {
        levelPresenter.OnPresenterLevelCompletedTriggered -= HandleOnPresenterLevelCompletedTriggered;
        LoadNextLevel(levelModel);
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
