using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private bool isCollected = false;
    public GameObject instructionScreen;
    public GameObject timer, timerIcon;
    public LettuceTimer TimerScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player 1
        if (collision.CompareTag("Player_01"))
        {
            player_movement ham = collision.GetComponent<player_movement>();
            if (ham != null)
            {
                ham.ActivatePowerup();
                timer.SetActive(true);
                timerIcon.SetActive(true);
                TimerScript.StartTimer();
            }

            isCollected = true;

            Destroy(gameObject);
        }
        //player 2 
        else if (collision.CompareTag("Player_02"))
        {
            player_movement ham2 = collision.GetComponent<player_movement>();
            if (ham2 != null)
            {
                ham2.ActivatePowerup();
                timer.SetActive(true);
                timerIcon.SetActive(true);
                TimerScript.StartTimer();
            }
            isCollected = true;

            Destroy(gameObject);
        }
    }

 
}
