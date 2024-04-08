using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePowerup : MonoBehaviour
{
    private bool isCollected = false;
    private float freezeDuration = 10f;
    public GameObject timerBottle, sleepIcon;
    public BottleTimer TimerScript;
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
                    timerBottle.SetActive(true);
                    sleepIcon.SetActive(true);
                    TimerScript.StartTimer();

                }
            }
        }
    }

}
