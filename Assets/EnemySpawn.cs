using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using static Cinemachine.CinemachineTargetGroup;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform[] targets;

    NavMeshAgent agent;
    public GameObject player1;
    public GameObject player2;
    public float speed;

    private float distance1;
    private float distance2;

    //powerup specs
    private bool isFrozen = false;
    private float freezeTimer = 0f;

    //hiding
    private Player_Movement_1 playerMovement;
    //animation 
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //hiding
        playerMovement = FindObjectOfType<Player_Movement_1>();
        agent = GetComponent<NavMeshAgent>();
        //animation
        if (animator == null)
        {
            Debug.LogError("Animator reference is not set on EnemySpawn script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance1 = Vector3.Distance(transform.position, player1.transform.position);
        distance2 = Vector3.Distance(transform.position, player2.transform.position);
        GameObject chaseTarget = distance1 > distance2 ? player2 : player1;

        // Check if the player is visible and within attack range
        if (playerMovement != null && playerMovement.IsPlayerVisibleToEnemy() && IsPlayerWithinRange(chaseTarget.transform.position))
        {
            // Attacks the player
            AttackPlayer(chaseTarget.transform);
        }
        else if (!playerMovement.IsHidden()) // Check if the player is not hidden
        {
            // Player is not hidden and is visible to the enemy, so chase the player
            agent.SetDestination(chaseTarget.transform.position);
        }
        else
        {
            GameObject otherPlayer = chaseTarget == player1 ? player2 : player1;
            agent.SetDestination(otherPlayer.transform.position);
        }

        UpdateFreezeTime();
        Vector3 movementDirection = agent.velocity.normalized;

        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("speed", movementDirection.sqrMagnitude);


    }
    public void AttackPlayer(Transform playerTransform)
    {

    }
    //checks if player is within range
    public bool IsPlayerWithinRange(Vector3 playerPosition)
    {

        float attackRange = 10f;
        return Vector3.Distance(transform.position, playerPosition) <= attackRange;
    }



    public void UpdateFreezeTime()
    {
        //baby powerup
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;

            if (freezeTimer <= 0f)
            {
                Unfreeze();
            }
        }
    }

    public void FreezeBaby(float duration)
    {
        if (!isFrozen)
        {
            isFrozen = true;
            freezeTimer = duration;
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            Debug.Log("enemy frozen for " + duration);
        }

    }

    private void Unfreeze()
    {
        isFrozen = false;
        agent.SetDestination(transform.position);
        agent.velocity = Vector3.zero;
        agent.isStopped = false;
        Debug.Log("baby unfrozen");
    }

}




