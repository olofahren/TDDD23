using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{

    public void StartGame()
    {
        PlayerPrefs.SetFloat("PlayerX", -2.98f);
        PlayerPrefs.SetFloat("PlayerY", -60.02f);
        PlayerPrefs.SetFloat("PlayerZ", -5.0f);
        PlayerPrefs.SetInt("BattleIsFinished", 0);
        SceneManager.LoadScene("World1");

    }

}
