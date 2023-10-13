using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class detectPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public float detectionDistance = 5.0f;
    private float orgDetectionDistance;
    public GameObject Player;
    public int battleNumber;
    private Animator anim;
    private Renderer rend;
    public GameObject controlPanel;
    public float delay = 3.0f;
    float timer = 0.0f;
    private GameObject player;
    private List<int> completedBattles;
    private Vector3 lockedPos;
    Rigidbody2D rb;
    private Vector3 newSpawnSpot;
    private Vector2 playerTestTransfrom;
    private NavMeshAgent agent;

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
        agent = transform.GetComponentInParent<NavMeshAgent>();

        orgDetectionDistance = detectionDistance;

    }

    void FixedUpdate()
    {
        if(completedBattles[battleNumber] == 1)
        {
            enemy.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (IsPlayerNear() && completedBattles[battleNumber] == 0)
        {
            
            rb.simulated = false;
            if (agent)
            {
                agent.speed = 0;
            }
            anim.SetTrigger("triggerBattle");
            rend.sortingOrder = 1;
            timer += Time.deltaTime;
            
            if(detectionDistance < orgDetectionDistance + 2)
            {
                detectionDistance = detectionDistance + 2;
            }

            if (timer > delay)
            {

                //Sets the player spawn position after the battle outside of the detection zone of the enemy
                Vector2 playerTestTransfrom = Player.transform.position;
                Vector2 transformPosition = transform.position;
                Vector2 direction = playerTestTransfrom - transformPosition;
                direction = direction.normalized;
                while (Vector2.Distance(playerTestTransfrom, transform.position) < detectionDistance+1)
                {

                    Debug.Log(Vector2.Distance(playerTestTransfrom, transform.position));
                    playerTestTransfrom += direction * 1.1f;
                }



                
                PlayerPrefs.SetFloat("PlayerX", playerTestTransfrom.x);
                PlayerPrefs.SetFloat("PlayerY", playerTestTransfrom.y);
                PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
                PlayerPrefs.SetString("currentWorld", "Main World");
                PlayerPrefs.SetInt("currentBattle", battleNumber);
                SceneManager.LoadScene("Battle2");
                PlayerPrefs.SetString("EnemyUnitType", enemy.enemyUnit);
                rb.simulated = true;
                if(agent) agent.speed = 0;
                detectionDistance = detectionDistance - 2;

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

  
