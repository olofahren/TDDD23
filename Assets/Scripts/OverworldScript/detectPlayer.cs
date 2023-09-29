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
    private Renderer rend;
    private PlayableDirector director;
    public GameObject controlPanel;
    public float delay = 10.0f;
    float timer = 0.0f;
    private GameObject player;
    private int battleIsFinished;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rend  = GetComponentInChildren<Renderer>();
        player = GameObject.Find("Player");
        battleIsFinished = PlayerPrefs.GetInt("BattleIsFinished");



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsPlayerNear() && battleIsFinished == 0)
        {
            //Debug.Log(timer);
            anim.SetTrigger("triggerBattle");
            rend.sortingOrder = 1;
            timer += Time.deltaTime;
            if (timer > delay)
            {
                PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
                PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
                PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
                PlayerPrefs.SetInt("BattleIsFinished", 1);
                SceneManager.LoadScene("Battle2");
            }
        }
    }

    private bool IsPlayerNear()
    {
        float dist = Vector2.Distance(Player.transform.position, transform.position);
        //Debug.Log(dist);
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

  
