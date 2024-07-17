using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeDisplay;

    private DateTime realStartTime;
    private float virtualElapsedTime;
    private float lastRealElapsedTime;

    void Start()
    {
        realStartTime = DateTime.Now;
        lastRealElapsedTime = 0.0f;
    }

    void Update()
    {
        if (realStartTime != null)
        {
            float realElapsedTime = (float)(DateTime.Now - realStartTime).TotalSeconds;
            virtualElapsedTime += realElapsedTime - lastRealElapsedTime;
            lastRealElapsedTime = realElapsedTime;

            DateTime currentVirtualTime = realStartTime.AddSeconds(virtualElapsedTime);
            timeDisplay.text = currentVirtualTime.ToString("HH:mm:ss");
        }
    }
}
