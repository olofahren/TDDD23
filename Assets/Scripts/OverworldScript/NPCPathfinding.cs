using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCPathfinding : MonoBehaviour
{
    [SerializeField] Transform target;


    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = false;
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
    }

    private void Update()
    {
        agent.SetDestination(target.position);
        transform.position = agent.nextPosition;
        //Debug.Log(Mathf.Abs(target.position.x - agent.nextPosition.x) + Mathf.Abs(target.position.y - agent.nextPosition.y));
        if ((Mathf.Abs(target.position.x - agent.nextPosition.x) + Mathf.Abs(target.position.y - agent.nextPosition.y)) > 5)
        {
           agent.speed = 10.0f;

        }
        else
        {
            agent.speed = 5.0f;
        }

    }
}
