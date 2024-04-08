using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BottleTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;

    [SerializeField] float BottlestartingTime = 10f;
    private float BottleremainingTime;
    private bool BottletimerRunning = false;

    public void StartTimer()
    {
        BottleremainingTime = BottlestartingTime;
        BottletimerRunning = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (BottleremainingTime > 0)
        {
            BottleremainingTime -= Time.deltaTime;
        }
        if (BottleremainingTime < 0)
        {
            BottleremainingTime = 0;

            //time over - baby is up!
            BottletimerRunning = false;
            gameObject.SetActive(false); // Disabling the timer GameObject when time reaches 0
        }



        int minutes = Mathf.FloorToInt(BottleremainingTime / 60);
        int seconds = Mathf.FloorToInt(BottleremainingTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}