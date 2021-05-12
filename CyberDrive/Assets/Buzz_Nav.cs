using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Buzz_Nav : MonoBehaviour
{
    public Transform goal;
    public NavMeshAgent agent;
    public float distance;
    public float chaseDistance;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= chaseDistance)
        {
            if (agent.isStopped == true)
            {
                agent.isStopped = false;
            }
            agent.destination = goal.position;
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
