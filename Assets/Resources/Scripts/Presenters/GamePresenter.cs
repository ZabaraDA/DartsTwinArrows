using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class GamePresenter : IGamePresenter, IInitializable, IDisposable
{
    private IGameView _view;
    private IGameModel _model;
    private IStatisticsPresenter _statisticsPresenter;
    private ILevelFactory _levelFactory;
    private bool _isDisposed = false;
    private ILevelPresenter _currentLevelPresenter;

    public GamePresenter(
        IGameView view,
        IGameModel model,
        IStatisticsPresenter statisticsPresenter,
        ILevelFactory levelFactory)
    {
        _view = view ?? throw new ArgumentNullException(nameof(view));
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _statisticsPresenter = statisticsPresenter;
        _levelFactory = levelFactory ?? throw new ArgumentNullException(nameof(levelFactory));
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;

        // Очищаем текущий уровень
        if (_currentLevelPresenter != null)
        {
            try
            {
                _currentLevelPresenter.OnPresenterLevelCompletedTriggered -= HandleOnPresenterLevelCompletedTriggered;
                _currentLevelPresenter.Dispose();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error disposing level presenter: {ex}");
            }
            _currentLevelPresenter = null;
        }

        // Отписываемся от событий View
        if (_view != null)
        {
            try
            {
                _view.OnViewPauseButtonClicked -= HandleOnViewPauseButtonClicked;
                _view.OnViewContinueButtonClicked -= HandleOnViewContinueButtonClicked;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error unsubscribing from view events: {ex}");
            }
            _view = null;
        }

        // Отписываемся от событий Model
        if (_model != null)
        {
            try
            {
                _model.OnModelCurrentLevelModelChanged -= HandleOnModelCurrentLevelModelChanged;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error unsubscribing from model events: {ex}");
            }
            _model = null;
        }

        _statisticsPresenter = null;
        _levelFactory = null;

        var destroyProjectiles = GameObject.FindGameObjectsWithTag("Level");
        foreach (var destroyProjectile in destroyProjectiles)
        {
            MonoBehaviour.Destroy(destroyProjectile);
        }
    }

    public void Initialize()
    {
        if (_isDisposed)
        {
            Debug.LogWarning("Attempt to initialize disposed GamePresenter");
            return;
        }

        try
        {
            // Отписываемся перед повторной подпиской
            if (_view != null)
            {
                _view.OnViewPauseButtonClicked -= HandleOnViewPauseButtonClicked;
                _view.OnViewContinueButtonClicked -= HandleOnViewContinueButtonClicked;
            }

            if (_model != null)
            {
                _model.OnModelCurrentLevelModelChanged -= HandleOnModelCurrentLevelModelChanged;
            }

            // Подписываемся на события
            _view.OnViewPauseButtonClicked += HandleOnViewPauseButtonClicked;
            _view.OnViewContinueButtonClicked += HandleOnViewContinueButtonClicked;
            _model.OnModelCurrentLevelModelChanged += HandleOnModelCurrentLevelModelChanged;

            // Загружаем текущий уровень
            if (_model?.CurrentLevelModel != null)
            {
                LoadLevel(_model.CurrentLevelModel);
            }
            else
            {
                Debug.LogError("CurrentLevelModel is null in Initialize");
            }

            Debug.Log("GamePresenter initialized");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error initializing GamePresenter: {ex}");
            throw;
        }
    }

    private void HandleOnModelCurrentLevelModelChanged(ILevelModel levelModel)
    {
        if (_isDisposed || levelModel == null) return;

        try
        {
            LoadLevel(levelModel);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in HandleOnModelCurrentLevelModelChanged: {ex}");
        }
    }

    private void LoadLevel(ILevelModel levelModel)
    {
        if (_isDisposed)
        {
            Debug.LogWarning("Attempt to load level on disposed GamePresenter");
            return;
        }

        if (_view == null || _view.Equals(null))
        {
            Debug.LogWarning("View is null in LoadLevel");
            return;
        }

        if (levelModel == null)
        {
            Debug.LogError("LoadLevel: levelModel is null");
            return;
        }

        try
        {
            // Очищаем предыдущий уровень
            if (_currentLevelPresenter != null)
            {
                _currentLevelPresenter.OnPresenterLevelCompletedTriggered -= HandleOnPresenterLevelCompletedTriggered;
                _currentLevelPresenter.Dispose();
                _currentLevelPresenter = null;
            }

            // Создаем новый уровень
            _currentLevelPresenter = _levelFactory?.Create(levelModel);
            if (_currentLevelPresenter == null)
            {
                Debug.LogError("Failed to create LevelPresenter");
                return;
            }

            _currentLevelPresenter.OnPresenterLevelCompletedTriggered += HandleOnPresenterLevelCompletedTriggered;

            // Загружаем и устанавливаем фон
            var backgroundPath = $"Images/Level {levelModel.Number} Background";
            var background = Resources.Load<Sprite>(backgroundPath);

            if (background == null)
            {
                Debug.LogError($"Background not found: Level {levelModel.Number}");
                background = Resources.Load<Sprite>("Images/DefaultBackground");
            }

            if (_view != null && !_view.Equals(null))
            {
                _view.SetImageBackground(background);
                _view.SetLevelText($"LEVEL {levelModel.Number}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in LoadLevel: {ex}");
        }
    }

    private void LoadNextLevel(ILevelModel levelModel)
    {
        if (_isDisposed)
        {
            Debug.LogWarning("Attempt to load next level on disposed GamePresenter");
            return;
        }

        if (_model == null || _model.Equals(null))
        {
            Debug.LogWarning("Model is null in LoadNextLevel");
            return;
        }

        if (levelModel == null)
        {
            Debug.LogError("LoadNextLevel: levelModel is null");
            return;
        }

        Debug.Log("LOADNEXTLEVEL");

        try
        {
            ILevelModel nextLevelModel = _model.LevelModels?.FirstOrDefault(x => x.Number == (levelModel.Number + 1));

            if (nextLevelModel != null)
            {
                _model.CurrentLevelModel = nextLevelModel;
            }
            else
            {
                // Игра завершена, возвращаемся на первый уровень
                var background = Resources.Load<Sprite>("Images/Level 1 Background");
                if (_view != null && !_view.Equals(null))
                {
                    _view.SetImageBackground(background);
                }

                _statisticsPresenter?.StopTimer();
                _statisticsPresenter?.ChangeVisibilityStatisticsPanel(true);
            }
        }
        catch (Exception ex)
        {   
            Debug.LogError($"Error in LoadNextLevel: {ex}");
        }
    }

    private void HandleOnPresenterLevelCompletedTriggered(ILevelPresenter levelPresenter, ILevelModel levelModel)
    {
        if (_isDisposed) return;

        try
        {
            if (levelPresenter != null)
            {
                levelPresenter.OnPresenterLevelCompletedTriggered -= HandleOnPresenterLevelCompletedTriggered;
            }
            LoadNextLevel(levelModel);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in HandleOnPresenterLevelCompletedTriggered: {ex}");
        }
    }

    private void HandleOnViewPauseButtonClicked()
    {
        if (_isDisposed || _view == null) return;

        try
        {
            Time.timeScale = 0f;
            _view.SetActivePausePanel(true);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in HandleOnViewPauseButtonClicked: {ex}");
        }
    }

    private void HandleOnViewContinueButtonClicked()
    {
        if (_isDisposed || _view == null) return;

        try
        {
            _view.SetActivePausePanel(false);
            Time.timeScale = 1.0f;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in HandleOnViewContinueButtonClicked: {ex}");
        }
    }
}