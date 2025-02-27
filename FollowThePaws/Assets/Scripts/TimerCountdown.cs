using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerCountdown : MonoBehaviour
{
    public float TimeLeft = 300;
    public bool TimerOn = false;

    public Text TimerText;
    
    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
            }
            else
            {
                TimeLeft = 300;
                TimerOn = false;
                TimerText.text = "00:00";
                return;
            }
            DisplayTime(TimeLeft);
        }
    }

    public void SetTimer(bool flag)
    {
        if (flag)
            TimeLeft = 300;

        TimerOn = flag;
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
