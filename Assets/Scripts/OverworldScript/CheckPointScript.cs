using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.UI.CanvasScaler;

public class CheckPointScript : MonoBehaviour
{

    public float detectionDistance = 5.0f;
    public GameObject Player;
    public BattleFunctions battleFunctions;
    public bool hasBeenVisited = false;

    private Renderer spriteRenderer;

    public float delay = 3.0f;
    private float timer = 0.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<Renderer>();
    }


    private void FixedUpdate()
    {
        if (IsPlayerNear() && !hasBeenVisited)
        {
            spriteRenderer.enabled = true;
            for (int unitNr = 1; unitNr <= 3; unitNr++)
            {
                PlayerPrefs.SetInt("Chicken" + unitNr + "cHP", PlayerPrefs.GetInt("Chicken" + unitNr + "maxHP"));
                PlayerPrefs.SetInt("Chicken" + unitNr + "nrSpA", PlayerPrefs.GetInt("Chicken" + unitNr + "maxNrSpA"));
                PlayerPrefs.SetInt("Chicken" + unitNr + "nrHeal", PlayerPrefs.GetInt("Chicken" + unitNr + "maxNrHeal"));
            }

            PlayerPrefs.SetFloat("PlayerXCheckpoint", Player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerYCheckpoint", Player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZCheckpoint", Player.transform.position.z);

            hasBeenVisited = true;

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
