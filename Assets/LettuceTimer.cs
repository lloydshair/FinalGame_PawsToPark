using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LettuceTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;

    [SerializeField] float startingTime = 10f;
    private float remainingTime;
    private bool timerRunning = false;
    public void StartTimer()
    {
        remainingTime = startingTime;
        timerRunning = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        if (remainingTime < 0)
        {
            remainingTime = 0;

            //time over 
            timerRunning = false;
            gameObject.SetActive(false); // Disabling the timer GameObject when time reaches 0
        }

        


        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}