using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{

    public float moveSpeed;

    private Rigidbody2D rbHamster;
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    private bool isMoving;
    Vector3 input2;
    private bool isHit;

    private bool isPowerActive = false;
    private float powerDuration = 30f;
    private float powerTimer = 0f;


    public GameObject gameOverScreen;

    public GameObject codePanel, closeDoor, openDoor, riddlePanel;
    private bool boxIsHit;

    public static bool isDoorOpen = false;





    // Start is called before the first frame update
    void Start()
    {
        rbHamster = GetComponent<Rigidbody2D>();
        codePanel.SetActive(false);
        closeDoor.SetActive(true);
        openDoor.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left))
        {
            rbHamster.velocity = new Vector2(-moveSpeed, rbHamster.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            rbHamster.velocity = new Vector2(moveSpeed, rbHamster.velocity.y);
        }
        else
        {
            // rbHamster.velocity = new Vector2(0, rbHamster.velocity.x);
            rbHamster.velocity = new Vector2(0, rbHamster.velocity.y);
        }


        if (Input.GetKey(up))
        {
            rbHamster.velocity = new Vector2(rbHamster.velocity.x, moveSpeed);
        }
        else if (Input.GetKey(down))
        {
            rbHamster.velocity = new Vector2(rbHamster.velocity.x, -moveSpeed);
        }
        else
        {
            rbHamster.velocity = new Vector2(rbHamster.velocity.x, 0);
        }

        if(isDoorOpen)
        {
            codePanel.SetActive(false);
            closeDoor.SetActive(false);
            openDoor.SetActive(true);

        }

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

        Debug.Log("door was hit");
        if(collision.tag =="door" && !isDoorOpen)
        {
            codePanel.SetActive(true);
        }

        Debug.Log("door was hit");
        if (collision.tag == "puzzlebox" && !boxIsHit)
        {
            riddlePanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("door"))
        {
            codePanel.SetActive(false);

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


}

