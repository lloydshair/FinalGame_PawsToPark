using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hamster2Control : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;
    Vector3 input2;
    private bool isHit;

    private bool isPowerActive = false;
    private float powerDuration = 10f;
    private float powerTimer = 0f;


    public GameObject gameOverScreen;
    public GameObject instructionScreen;
    public GameObject instructionScreen2;

    private void Update()
    {
        if (gameObject.CompareTag("Player_02") && !isMoving)
        {
            float moveHorizontal = 0f;
            float moveVertical = 0f;

            // Check if keys are pressed
            if (Input.GetKey(KeyCode.D))
                moveHorizontal = 1f;
            else if (Input.GetKey(KeyCode.A))
                moveHorizontal = -1f;

            if (Input.GetKey(KeyCode.W))
                moveVertical = 1f;
            else if (Input.GetKey(KeyCode.S))
                moveVertical = -1f;

            
            // Calculate the input vector
            input2 = new Vector3(moveHorizontal, moveVertical, 0f);

            //powerups
            if (isPowerActive)
            {
                powerTimer -= Time.deltaTime;

                if (powerTimer <= 0f)
                {
                    DeactivatePowerup();
                }
            }

        }
    }

    private void FixedUpdate()
    {
        if (input2 != Vector3.zero)
        {
            // Calculate the target position
            GetComponent<Rigidbody2D>().velocity = input2 * moveSpeed; ;

          
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        // Ensure that the position is exactly at the target position
        transform.position = targetPos;
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("baby was hit");
        if (collision.tag == "baby_0" && !isHit)
        {
            isHit = true;
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;


        }

        if (collision.tag == "Lettuce")
        {
            StartCoroutine(ShowInstruction());

        }

        if (collision.tag == "Bottle")
        {
            StartCoroutine(ShowInstruction2());

        }
    }

    public void ActivatePowerup()
    {
        if (!isPowerActive)
        {
            moveSpeed += 3f;
            isPowerActive = true;
            powerTimer = powerDuration;
            Debug.Log("Powerup collect with speed " + moveSpeed);
        }
        else
        {
            powerTimer = powerDuration;

        }
    }

    private void DeactivatePowerup()
    {
        moveSpeed -= 3f;
        isPowerActive = false;
        Debug.Log("Powerup gone speed gone back to " + moveSpeed);
    }

    IEnumerator ShowInstruction()
    {

        instructionScreen.SetActive(true);

        yield return new WaitForSeconds(4f);

        instructionScreen.SetActive(false);

    }

    IEnumerator ShowInstruction2()
    {
        instructionScreen2.SetActive(true);

        yield return new WaitForSeconds(4f);

        instructionScreen2.SetActive(false);
    }
}
