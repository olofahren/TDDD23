using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class detectPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public float detectionDistance = 5.0f;
    public GameObject Player;
    private Animator anim;
    private PlayableDirector director;
    public GameObject controlPanel;
    public float delay = 10.0f;
    float timer;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlayerNear())
        {
            Debug.Log("PLAYER WITHIN ZONE");
            anim.SetTrigger("triggerBattle");
            timer += Time.deltaTime;
            if (timer > delay)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private bool isPlayerNear()
    {
        float dist = Vector2.Distance(Player.transform.position, transform.position);
        Debug.Log(dist);
        if (dist < detectionDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}

  
