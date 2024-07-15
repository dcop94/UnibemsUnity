using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeDisplay;
    public Button fastForwardButton;
    public Button normalSpeedButton;

    private DateTime realStartTime;
    private float virtualElapsedTime;
    private float lastRealElapsedTime;
    private float timeScale = 1.0f;

    void Start()
    {
        realStartTime = DateTime.Now;
        lastRealElapsedTime = 0.0f;

        if (fastForwardButton != null )
        {
            fastForwardButton.onClick.AddListener(SetFastForward);
        }
        if ( normalSpeedButton != null )
        {
            normalSpeedButton.onClick.AddListener(SetNormalSpeed);
        }
    }
        

    void Update()
    {
        if (realStartTime != null)
        {
            float realElapsedTime = (float)(DateTime.Now - realStartTime).TotalSeconds;
            virtualElapsedTime += (realElapsedTime - lastRealElapsedTime) * timeScale;
            lastRealElapsedTime = realElapsedTime;

            DateTime currentVirtualTime = realStartTime.AddSeconds(virtualElapsedTime);
            timeDisplay.text = currentVirtualTime.ToString("HH:mm:ss");
        }
    }


    void SetNormalSpeed()
    {
        UpdateVirtualElapsedTime();
        timeScale = 1.0f;
    }

    void SetFastForward()
    {
        UpdateVirtualElapsedTime();
        timeScale = 2.0f;
    }

    private void UpdateVirtualElapsedTime()
    {
        float realElapsedTime = (float)(DateTime.Now - realStartTime).TotalSeconds;
        virtualElapsedTime += (realElapsedTime - lastRealElapsedTime) * timeScale;
        lastRealElapsedTime = realElapsedTime;
    }
}
