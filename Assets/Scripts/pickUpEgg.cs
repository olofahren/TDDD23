using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class pickUpEgg : MonoBehaviour
{
    public float detectionDistance = 2.0f;
    private Animator anim;
    public GameObject player;
    private SpriteRenderer spriteRenderer;
    public bool playerHasPickedUpEgg = false;
    private List<int> collectedEggs;
    private int eggName; 
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collectedEggs = PlayerPrefsExtra.GetList<int>("collectedEggs2");
        eggName = Int32.Parse(gameObject.name);

        Debug.Log("-pickUpEgg- says: Collected eggs count: " + collectedEggs.Count);


        if (collectedEggs[eggName] == 1)
        {
            spriteRenderer.enabled = false;
            playerHasPickedUpEgg = true;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (IsPlayerNear() && collectedEggs[eggName] == 0)
        {
            collectedEggs = PlayerPrefsExtra.GetList<int>("collectedEggs2");
            spriteRenderer.enabled = true;
            playerHasPickedUpEgg = false;
        }



        if (IsPlayerNear() && !playerHasPickedUpEgg)
        {
            collectedEggs = PlayerPrefsExtra.GetList<int>("collectedEggs2");
            collectedEggs[eggName] = 1;

            PlayerPrefsExtra.SetList("collectedEggs2", collectedEggs);
            spriteRenderer.enabled = false;
            playerHasPickedUpEgg = true;
            //Debug.Log("Player has picked upp egg " + eggName);
        }
    }

    private bool IsPlayerNear()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
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
