using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Movement_1 : MonoBehaviour
{
   //animation 
    
    Animator anim;
    float dirX, moveSpeed;
    bool facingRight = true;
    Vector3 localScale;


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
    public GameObject instructionScreen;
    public GameObject instructionScreen2;


    public GameObject codePanel, closeDoor, openDoor, riddlePanel, noKeyPanel;

    private bool boxIsHit;

    public static bool isDoorOpen = false;

    // hiding feature
    public SpriteRenderer custRender;
    private bool isHidden = false;
    private bool isPlayerVisibleToEnemy = true;
    public LayerMask enemyLayer;
    private Collider2D playerCollider;

   
    void Start()
    {
        rbHamster = GetComponent<Rigidbody2D>();
        codePanel.SetActive(false);
        closeDoor.SetActive(true);
        openDoor.SetActive(false);
        //hiding
        custRender = GetComponent<SpriteRenderer>();
        custRender.enabled = true;
        rbHamster = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        //animation
         anim = GetComponent<Animator>();
        localScale = transform.localScale;
    }

    
    void Update()
    {

        //aniamtion 

        if (isPowerActive && moveSpeed < 4f)
        {
            moveSpeed = 4f;
        }
        else
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
                moveSpeed = 2f;
            else
                moveSpeed = 2f;
        }

        SetAnimationState();
        dirX = Input.GetAxisRaw("Horizontal");

        rbHamster.velocity = new Vector2(dirX * moveSpeed, rbHamster.velocity.y);

        // Flip the player sprite if moving left or right
        if (dirX != 0)
        {
            facingRight = dirX > 0;
            FlipSprite();
        }

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

        if (isDoorOpen)
        {
            codePanel.SetActive(false);
            closeDoor.SetActive(false);
            openDoor.SetActive(true);

        }

        if (codePanel.activeSelf || riddlePanel.activeSelf || noKeyPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f; 
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

        if (codePanel.activeSelf || riddlePanel.activeSelf || noKeyPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f; 
        }

        // hiding 
        UpdateVisibilityToEnemy();
        UpdateHiding();
    }
    void FlipSprite()
    {
        // Flip the player sprite horizontally if facing left
        transform.localScale = new Vector3(facingRight ? localScale.x : -localScale.x, localScale.y, localScale.z);
    }

    //animation
    void SetAnimationState()
    {
        anim.SetBool("walking", Mathf.Abs(dirX) > 0);
        anim.SetBool("running", isPowerActive);
    }


     void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
                localScale.x *= -1;

            transform.localScale = localScale;
    }

    void UpdateVisibilityToEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, enemyLayer);

        if (hit.collider != null)
            isPlayerVisibleToEnemy = true;
        else
            isPlayerVisibleToEnemy = false;
    }

    void UpdateHiding()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isHidden = !isHidden;
            custRender.enabled = !isHidden;
            // Disables the player's collider when hidden
            playerCollider.enabled = !isHidden;
        }
    }

    // Method to check if the player is hidden
    public bool IsHidden()
    {
        return isHidden;
    }

    public bool IsPlayerVisibleToEnemy()
    {
        return isPlayerVisibleToEnemy;
    }



    private void FixedUpdate()
    {
        if (input2 != Vector3.zero)
        {
            // Calculates the target position
            GetComponent<Rigidbody2D>().velocity = input2 * moveSpeed; ;


        }
        
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // Moves towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
       
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
        if (collision.tag == "door" && !isDoorOpen)
        {
            codePanel.SetActive(true);
        }

        Debug.Log("box was hit");
        if (collision.tag == "puzzleBox" && !boxIsHit)
        {
            riddlePanel.SetActive(true);
        }


        Debug.Log("box was hit");
        if (collision.tag == "openBox" && !boxIsHit)
        {
            noKeyPanel.SetActive(true);
        }

        if (collision.tag == "Lettuce")
        {
            Debug.Log("Lettuce collision detected!");
            ActivatePowerup();
        }

        if (collision.tag == "Bottle")
        {
            StartCoroutine(ShowInstruction2());

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("door"))
        {
            codePanel.SetActive(false);

        }
        if (collision.tag == "Lettuce")
        {
            Debug.Log("Lettuce powerup detected!"); // Add this line
            ActivatePowerup();
        }

    }

    public void ActivatePowerup()
    {
        if (!isPowerActive)
        {
            moveSpeed += 3f;
            isPowerActive = true;
            powerTimer = powerDuration;
            Debug.Log("Powerup activated. New moveSpeed: " + moveSpeed);
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

