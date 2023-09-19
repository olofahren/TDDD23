using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public bool facingRight;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator> ();
        moveSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ProcecessInputs();
    }

    void FixedUpdate()
    {
        Move();

        
    }

    void  ProcecessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //Change animation if player is walking
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //Flip player if walking to the left while keeping the right scale of the character
        if (rb.velocity.x < 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}