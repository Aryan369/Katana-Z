using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("COMPONENT")]
    public TextMeshProUGUI timerText;
    public Gradient gradient;

    [Header("TIMER SETTINGS")]
    public float currentTime;
    public bool countDown = true;

    [Header("LIMIT SETTINGS")]
    public bool hasLimits;
    public float timerLimit;

    [Header("FORMAT SETTINGS")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethDecimal, "0.00");
    }

    void Update()
    {
        currentTime = countDown ? currentTime - Time.deltaTime : currentTime + Time.deltaTime;

        if (hasLimits && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            Player.Instance.KillPlayer();
        }

        SetTimerText();
        timerText.color = gradient.Evaluate(countDown ? Mathf.Clamp01(currentTime - timerLimit) : Mathf.Clamp01(timerLimit - currentTime));
    }

    void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethDecimal
}
