using TMPro;
using UnityEngine;

public class StatisticsView : MonoBehaviour, IStatisticsView
{
    [SerializeField]
    private TMP_Text _timeText;
    [SerializeField]
    private TMP_Text _accurancyText;
    [SerializeField]
    private TMP_Text _pointsText;

    public void SetAccurancyText(string text)
    {
        _accurancyText.text = text;
    }

    public void SetPointsText(string text)
    {
        _pointsText.text = text;
    }

    public void SetTimeText(string text)
    {
        _timeText.text = text;
    }
}
