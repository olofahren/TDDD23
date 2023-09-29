using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    private List<int> collectedEggs = Enumerable.Repeat(1, 11).ToList();
    public void StartGame()
    {
        //collectedEggs.ForEach(egg => Debug.Log(egg));
        collectedEggs[0] = 0;
        PlayerPrefs.SetFloat("PlayerX", -2.98f);
        PlayerPrefs.SetFloat("PlayerY", -60.02f);
        PlayerPrefs.SetFloat("PlayerZ", -5.0f);
        PlayerPrefs.SetInt("BattleIsFinished", 0);
        PlayerPrefsExtra.SetList("collectedEggs", collectedEggs);
        SceneManager.LoadScene("Intro World");

    }

}
