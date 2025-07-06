using System;
using UnityEngine;

public class StatisticsModel : IStatisticsModel
{
    private float _totalTime;
    public float TotalTime 
    {
        get => _totalTime;
        set
        {
            if (_totalTime != value)
            {
                _totalTime = value;
                OnModelTotalTimeChanged?.Invoke(_totalTime);
                Debug.Log($"Field '{nameof(TotalTime)}' changed in model");
            }
        }
    }

    public float Accuracy => Points / TotalPoints * 100;

    private int _points;
    public int Points
    {
        get => _points;
        set
        {
            if (_points != value)
            {
                _points = value;
                OnModelPointsChanged?.Invoke(_points);
                Debug.Log($"Field '{nameof(Points)}' changed in model");
            }
        }
    }
    private int _totalPoints;
    public int TotalPoints
    {
        get => _totalPoints;
        set
        {
            if (_totalPoints != value)
            {
                _totalPoints = value;
                OnModelTotalPointsChanged?.Invoke(_totalPoints);
                Debug.Log($"Field '{nameof(TotalPoints)}' changed in model");
            }
        }
    }

    public event Action<float> OnModelTotalTimeChanged;
    public event Action<int> OnModelPointsChanged;
    public event Action<int> OnModelTotalPointsChanged;

    public StatisticsModel(float totalTime, int points, int totalPoints)
    {
        Points = points;
        TotalTime = totalTime;
        TotalPoints = totalPoints;
    }

    public void AddPoints(int points)
    {
        Points += points;
    }

    public void AddTime(float time)
    {
        TotalTime += time;
    }

    public void AddTotalPoints(int points)
    {
        TotalPoints += points;
    }
}
