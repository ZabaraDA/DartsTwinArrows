using UnityEngine;

public interface IStatisticsView
{
    void SetTimeText(string text);
    void SetAccurancyText(string text);
    void SetPointsText(string text);
    void SetActive(bool isActive);
}
