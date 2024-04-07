using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePowerup : MonoBehaviour
{
    private bool isCollected = false;
    private float freezeDuration = 10f;
    public GameObject timer, timerIcon;
    public timer TimerScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected)
        {
            if (collision.CompareTag("Player_01") || collision.CompareTag("Player_02"))
            {
                EnemySpawn baby = FindAnyObjectByType<EnemySpawn>();
                if (baby != null)
                {
                    baby.FreezeBaby(freezeDuration);
                    isCollected = true;
                    Destroy(gameObject);
                    timer.SetActive(true);
                    timerIcon.SetActive(true);
                    TimerScript.StartTimer();

                }
            }
        }
    }

}
