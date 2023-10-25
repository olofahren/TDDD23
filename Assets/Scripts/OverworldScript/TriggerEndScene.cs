using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndScene : MonoBehaviour
{
    public float detectionDistance = 5.0f;
    public GameObject Player;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsPlayerNear())
        {
            SceneManager.LoadScene("Win World");
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
