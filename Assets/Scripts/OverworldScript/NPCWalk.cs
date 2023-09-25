using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    public Rigidbody2D rb;
    private Vector2 lastPosition;
    private bool facingRight;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log((Mathf.Abs(rb.position.x - lastPosition.x) + Mathf.Abs(rb.position.y - lastPosition.y)));
        if(rb.position != lastPosition)
        {
            if((Mathf.Abs(rb.position.x - lastPosition.x) + Mathf.Abs(rb.position.y - lastPosition.y)) > 0.21 || Input.GetButton("Sprint"))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);

            }


        }
        else
        {
            anim.SetBool("isWalking", false);
        }


        if(lastPosition.x > rb.position.x && !facingRight)
        {
            Flip();
        }
        if (lastPosition.x < rb.position.x && facingRight)
        {
            Flip();
        }



        lastPosition = rb.position;

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

}
