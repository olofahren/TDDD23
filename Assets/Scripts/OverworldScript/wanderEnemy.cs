using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wanderEnemy : MonoBehaviour
{

    private Vector3 target;
    public NavMeshAgent agent;
    Vector2 pos;
    public Vector2 maxPos;
    public Vector2 minPos;
    private float timer = 0.0f;
    private float delay = 10.0f;



    // Start is called before the first frame update
    void Start()
    {
        delay = Random.value * 10;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = false;
        agent.speed = 5.0f;
        target = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), transform.position.z);
        agent.SetDestination(target);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(target);
        transform.position = agent.nextPosition;


        timer += Time.deltaTime;
        if (timer > delay)
        {
            delay = Random.value * 20;
            timer = 0.0f;
            target = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), transform.position.z);
            agent.SetDestination(target);

        }


    }
}
