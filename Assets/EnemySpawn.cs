using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    public GameObject player1;
    public GameObject player2;
    public float speed;

    private float distance1;
    private float distance2;

    //powerup specs
    private bool isFrozen = false;
    private float freezeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance1= Vector2.Distance(transform.position,player1.transform.position);
        distance2 = Vector2.Distance(transform.position, player2.transform.position);
        Vector2 target;
        GameObject chaseTarget;

        //Vector2 direction = player1.transform.position - transform.position;
        if (distance1 > distance2)
        {
            chaseTarget = player2;
        }
        else  {
            chaseTarget = player1;
        }
        agent.SetDestination(chaseTarget.transform.position);

        UpdateFreezeTime();
        
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
            speed = 0f;
            Debug.Log("enemy frozen for " + duration);
        }
    
    }

    private void Unfreeze()
    {
        isFrozen = false;
        speed = 2f;
        Debug.Log("baby unfrozen");
    }
}
