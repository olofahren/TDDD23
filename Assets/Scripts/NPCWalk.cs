using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;


    public float moveSpeed;
    private Rigidbody2D myRidgidBody;
    public bool isWalking;
    private Vector2 position;
    private Vector2 newPosition;
    private float newXPos;
    private float xPos;
    private float newYPos;
    private float yPos;
    private float timer;
    public float targetTime = 3.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        isWalking = false;
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = gameObject.transform.position;
        xPos = position.x;

        yPos = position.y;

        if (position != newPosition)
        {
            targetTime = 3.0f;
            anim.SetBool("isWalking", true);
            isWalking = true;
            newPosition = gameObject.transform.position;


            //Debug.Log(newXPos - xPos);
            //Debug.Log(newYPos - yPos);
     
        

            newXPos = gameObject.transform.position.x;
            newYPos = gameObject.transform.position.y;




        }
        else
        {

            isWalking = false;
            anim.SetBool("isWalking", false);


        }

    }

    
}
