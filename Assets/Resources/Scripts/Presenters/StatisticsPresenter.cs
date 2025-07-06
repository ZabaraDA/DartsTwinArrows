using UnityEngine;

public class StatisticsPresenter : IStatisticsPresenter
{
    private IStatisticsView _view;
    private IStatisticsModel _model;

    public StatisticsPresenter(IStatisticsView view, IStatisticsModel model)
    {
        _view = view;
        _model = model;
    }
    public void Dispose()
    {
        _model.OnModelPointsChanged -= HandleOnModelPointsChanged;
        _model.OnModelTotalTimeChanged -= HandleOnModelTotalTimeChanged;
        _model.OnModelTotalPointsChanged -= HandleOnModelTotalPointsChanged;
    }

    public void Initialize()
    {
        _model.OnModelPointsChanged += HandleOnModelPointsChanged;
        _model.OnModelTotalTimeChanged += HandleOnModelTotalTimeChanged;
        _model.OnModelTotalPointsChanged += HandleOnModelTotalPointsChanged;

        _view.SetTimeText(_model.TotalTime.ToString());
        _view.SetAccurancyText(_model.Accuracy.ToString());
        _view.SetPointsText(_model.Points.ToString());
    }

    private void HandleOnModelTotalTimeChanged(float time)
    {
        _view.SetTimeText(time.ToString());
    }
    private void HandleOnModelTotalPointsChanged(int totalPoints)
    {
        _view.SetAccurancyText(_model.Accuracy.ToString());
    }
    private void HandleOnModelPointsChanged(int points)
    {
        _view.SetPointsText(points.ToString());
        _view.SetAccurancyText(_model.Accuracy.ToString());
    }

    public void Update(float updatableParameter)
    {
        _model.AddTime(updatableParameter);
    }
}
