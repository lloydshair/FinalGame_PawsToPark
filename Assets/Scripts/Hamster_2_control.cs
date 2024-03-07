using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hamster2Control : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;
    Vector3 input2;
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
}
