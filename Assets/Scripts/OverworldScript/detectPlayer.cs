using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class detectPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public float detectionDistance = 5.0f;
    public GameObject Player;
    public int battleNumber;
    private Animator anim;
    private Renderer rend;
    private PlayableDirector director;
    public GameObject controlPanel;
    public float delay = 3.0f;
    float timer = 0.0f;
    private GameObject player;
    private List<int> completedBattles;
    private Vector3 lockedPos;
    Rigidbody2D rb;

    public Unit enemy;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rend  = GetComponentInChildren<Renderer>();
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();
        completedBattles = PlayerPrefsExtra.GetList<int>("completedBattles");
        enemy = enemy.GetComponent<Unit>();
        lockedPos = Vector3.zero;

    }

    void FixedUpdate()
    {
        if(completedBattles[battleNumber] == 1)
        {
            enemy.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (IsPlayerNear() && completedBattles[battleNumber] == 0)
        {
            //Debug.Log(timer);
            if(lockedPos == Vector3.zero) { 
                lockedPos = player.transform.position;
            }
            rb.simulated = false;
            anim.SetTrigger("triggerBattle");
            rend.sortingOrder = 1;
            timer += Time.deltaTime;
            if (timer > delay)
            {
                PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
                PlayerPrefs.SetFloat("PlayerY", player.transform.position.y-2);
                PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
                PlayerPrefs.SetString("currentWorld", "Main World");
                PlayerPrefs.SetInt("currentBattle", battleNumber);
                SceneManager.LoadScene("Battle2");
                PlayerPrefs.SetString("EnemyUnitType", enemy.enemyUnit);
                rb.simulated = true;
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

  
