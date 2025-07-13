using UnityEngine;

public class StatisticsPresenter : IStatisticsPresenter
{
    private IStatisticsView _view;
    private IStatisticsModel _model;
    private IStatisticsLifeCycleManager _lifeCycleManager;

    public StatisticsPresenter(IStatisticsView view, IStatisticsModel model, IStatisticsLifeCycleManager lifeCycleManager)
    {
        _view = view;
        _model = model;
        _lifeCycleManager = lifeCycleManager;
    }
    public void Dispose()
    {
        _model.OnModelPointsChanged -= HandleOnModelPointsChanged;
        _model.OnModelTotalTimeChanged -= HandleOnModelTotalTimeChanged;
        _model.OnModelTotalPointsChanged -= HandleOnModelTotalPointsChanged;

        _lifeCycleManager.UnregisterPresenter(this);

        //if (_view as MonoBehaviour != null)
        //{
        //    MonoBehaviour.Destroy(_view.GetGameObject());
        //}
    }

    public void Initialize()
    {
        _model.OnModelPointsChanged += HandleOnModelPointsChanged;
        _model.OnModelTotalTimeChanged += HandleOnModelTotalTimeChanged;
        _model.OnModelTotalPointsChanged += HandleOnModelTotalPointsChanged;

        _view.SetTimeText(TimeFormatterService.FormatTimeMinutesSeconds(_model.TotalTime));
        _view.SetAccurancyText($"{_model.Accuracy}%");
        _view.SetPointsText(_model.Points.ToString());

        _lifeCycleManager.RegisterPresenter(this);
    }

    private void HandleOnModelTotalTimeChanged(float time)
    {
        _view.SetTimeText(TimeFormatterService.FormatTimeMinutesSeconds(time));
    }
    private void HandleOnModelTotalPointsChanged(int totalPoints)
    {
        _view.SetAccurancyText($"{_model.Accuracy}%");
    }
    private void HandleOnModelPointsChanged(int points)
    {
        _view.SetPointsText(points.ToString());
        _view.SetAccurancyText($"{_model.Accuracy}%");
    }

    public void Update(float updatableParameter)
    {
        if (_model.IsActive)
        {
            _model.AddTime(updatableParameter);
        }
    }

    public void ChangeVisibilityStatisticsPanel(bool isVisible)
    {
        _view.SetActive(isVisible);
    }

    public void StopTimer()
    {
        _model.IsActive = false;
    }

    public void AddPoints(int points)
    {
        _model.AddPoints(points);
    }

    public void AddTotalPoints(int points)
    {
        _model.AddTotalPoints(points);
    }
}
