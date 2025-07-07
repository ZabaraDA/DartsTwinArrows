using UnityEngine;

public static class TimeFormatterService
{
    /// <summary>
    /// ����������� ����� � �������� (float) � ��������������� ������ "MM:SS".
    /// </summary>
    /// <param name="timeInSeconds">����� � �������� (float).</param>
    /// <returns>������ � ������� "MM:SS".</returns>
    public static string FormatTimeMinutesSeconds(float timeInSeconds)
    {
        // ��������, ��� ����� �� �������������
        if (timeInSeconds < 0)
        {
            timeInSeconds = 0;
        }

        // ��������� ������
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);

        // ��������� ������� (������� �� ������� �� 60)
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // ����������� ������. "D2" �������� "������� ��� ������� 2 �����, �������� ������� ���� ��� �������������".
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    /// <summary>
    /// ����������� ����� � �������� (float) � ��������������� ������ "MM:SS.FF" (������:�������.������������).
    /// </summary>
    /// <param name="timeInSeconds">����� � �������� (float).</param>
    /// <param name="decimalPlaces">���������� ������ ����� ������� ��� ������ (�� ��������� 2).</param>
    /// <returns>������ � ������� "MM:SS.FF".</returns>
    public static string FormatTimeMinutesSecondsMilliseconds(float timeInSeconds, int decimalPlaces = 2)
    {
        if (timeInSeconds < 0)
        {
            timeInSeconds = 0;
        }

        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float remainingSeconds = timeInSeconds % 60;

        // ����������� ������� � ��������� ������
        string secondsFormat = "F" + decimalPlaces; // ��������, "F2" ��� ���� ������ ����� �������

        return string.Format("{0:D2}:{1:" + secondsFormat + "}", minutes, remainingSeconds);
    }
}
