using System;
using UnityEngine;

public interface IStatisticsModel
{
    float TotalTime { get; set; }
    float Accuracy { get; }
    int Points { get; set; }
    int TotalPoints { get; set; }

    event Action<float> OnModelTotalTimeChanged;
    event Action<int> OnModelPointsChanged;
    event Action<int> OnModelTotalPointsChanged;

    void AddTime(float time);
    void AddPoints(int points);
    void AddTotalPoints(int points);
}
