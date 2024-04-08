using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class level2hams2 : MonoBehaviour
{

    //animation 

    Animator anim;
    float dirX, moveSpeed2;
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
  




    private bool boxIsHit;



    // hiding feature
    public SpriteRenderer custRender;
    private bool isHidden = false;
    private bool isPlayerVisibleToEnemy = true;
    public LayerMask enemyLayer;
    private Collider2D playerCollider;


    void Start()
    {
        rbHamster = GetComponent<Rigidbody2D>();

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

     

        if (isPowerActive && moveSpeed2 < 4f)
        {
            moveSpeed2 = 4f;
        }
        else
        {

            if (Input.GetKey(KeyCode.LeftShift))
                moveSpeed2 = 2f;
            else
                moveSpeed2 = 2f;
        }

        SetAnimationState();
        dirX = Input.GetAxisRaw("Horizontal");

        rbHamster.velocity = new Vector2(dirX * moveSpeed2, rbHamster.velocity.y);

        // Flip the player sprite if moving left or right
        if (dirX != 0)
        {
            facingRight = dirX > 0;
            FlipSprite();
        }

        if (Input.GetKey(left))
        {
            rbHamster.velocity = new Vector2(-moveSpeed2, rbHamster.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            rbHamster.velocity = new Vector2(moveSpeed2, rbHamster.velocity.y);
        }
        else
        {

            rbHamster.velocity = new Vector2(0, rbHamster.velocity.y);
        }


        if (Input.GetKey(up))
        {
            rbHamster.velocity = new Vector2(rbHamster.velocity.x, moveSpeed2);
        }
        else if (Input.GetKey(down))
        {
            rbHamster.velocity = new Vector2(rbHamster.velocity.x, -moveSpeed2);
        }
        else
        {
            rbHamster.velocity = new Vector2(rbHamster.velocity.x, 0);
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
        anim.SetBool("Walking2", Mathf.Abs(dirX) > 0);
        anim.SetBool("Running2", isPowerActive);
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
        if (Input.GetKeyDown(KeyCode.E))
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
            GetComponent<Rigidbody2D>().velocity = input2 * moveSpeed2; ;


        }

    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // Moves towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed2 * Time.deltaTime);
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


        if (collision.tag == "Lettuce")
        {
            Debug.Log("Lettuce collision detected!");
            ActivatePowerup();
        }

       

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

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
            moveSpeed2 += 3f;
            isPowerActive = true;
            powerTimer = powerDuration;
            Debug.Log("Powerup activated. New moveSpeed: " + moveSpeed2);
        }
        else
        {
            powerTimer = powerDuration;
        }
    }

    private void DeactivatePowerup()
    {
        moveSpeed2 -= 3f;
        isPowerActive = false;
        Debug.Log("Powerup gone speed gone back to " + moveSpeed2);
    }

   
}





