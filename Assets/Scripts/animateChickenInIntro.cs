using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animateChickenInIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "Intro World")
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);

        }
        else if(SceneManager.GetActiveScene().name == "Win World")
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);


        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
