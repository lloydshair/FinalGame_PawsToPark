using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster1Control : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;
    Vector3 input;
    private void Update()
    {
        if (gameObject.CompareTag("Player_01") && !isMoving)
        {
            float moveHorizontal = 0f;
            float moveVertical = 0f;

            // Check if keys are pressed
            if (Input.GetKey(KeyCode.RightArrow))
                moveHorizontal = 1f;
            else if (Input.GetKey(KeyCode.LeftArrow))
                moveHorizontal = -1f;

            if (Input.GetKey(KeyCode.UpArrow))
                moveVertical = 1f;
            else if (Input.GetKey(KeyCode.DownArrow))
                moveVertical = -1f;

            // Calculate the input vector
            input = new Vector3(moveHorizontal, moveVertical, 0f);



           
        }
    }

    private void FixedUpdate()
    {
        if (input != Vector3.zero)
        {
           GetComponent<Rigidbody2D>().velocity = input*moveSpeed;
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
