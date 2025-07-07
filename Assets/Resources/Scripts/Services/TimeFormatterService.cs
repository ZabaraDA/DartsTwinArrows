using UnityEngine;

public static class TimeFormatterService
{
    /// <summary>
    /// Преобразует время в секундах (float) в форматированную строку "MM:SS".
    /// </summary>
    /// <param name="timeInSeconds">Время в секундах (float).</param>
    /// <returns>Строка в формате "MM:SS".</returns>
    public static string FormatTimeMinutesSeconds(float timeInSeconds)
    {
        // Убедимся, что время не отрицательное
        if (timeInSeconds < 0)
        {
            timeInSeconds = 0;
        }

        // Вычисляем минуты
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);

        // Вычисляем секунды (остаток от деления на 60)
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Форматируем строку. "D2" означает "вывести как минимум 2 цифры, добавляя ведущие нули при необходимости".
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    /// <summary>
    /// Преобразует время в секундах (float) в форматированную строку "MM:SS.FF" (минуты:секунды.миллисекунды).
    /// </summary>
    /// <param name="timeInSeconds">Время в секундах (float).</param>
    /// <param name="decimalPlaces">Количество знаков после запятой для секунд (по умолчанию 2).</param>
    /// <returns>Строка в формате "MM:SS.FF".</returns>
    public static string FormatTimeMinutesSecondsMilliseconds(float timeInSeconds, int decimalPlaces = 2)
    {
        if (timeInSeconds < 0)
        {
            timeInSeconds = 0;
        }

        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float remainingSeconds = timeInSeconds % 60;

        // Форматируем секунды с плавающей точкой
        string secondsFormat = "F" + decimalPlaces; // Например, "F2" для двух знаков после запятой

        return string.Format("{0:D2}:{1:" + secondsFormat + "}", minutes, remainingSeconds);
    }
}
