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

        // Player Unit Set Stats
        PlayerPrefs.SetInt("Chicken1Lvl", 1);
        PlayerPrefs.SetInt("Chicken2Lvl", 1);
        PlayerPrefs.SetInt("Chicken3Lvl", 1);

        PlayerPrefs.SetInt("Chicken1dmg", 1);
        PlayerPrefs.SetInt("Chicken2dmg", 1);
        PlayerPrefs.SetInt("Chicken3dmg", 1);

        PlayerPrefs.SetInt("Chicken1maxHP", 10);
        PlayerPrefs.SetInt("Chicken2maxHP", 10);
        PlayerPrefs.SetInt("Chicken3maxHP", 10);

        PlayerPrefs.SetInt("Chicken1cHP", 10); // Base Hp for lvl 1 is 10 HP
        PlayerPrefs.SetInt("Chicken2cHP", 10); // Base Hp for lvl 1 is 10 HP
        PlayerPrefs.SetInt("Chicken3cHP", 10); // Base Hp for lvl 1 is 10 HP

        PlayerPrefs.SetInt("Chicken1speed", 3);
        PlayerPrefs.SetInt("Chicken2speed", 5);
        PlayerPrefs.SetInt("Chicken3speed", 2);

        PlayerPrefs.SetInt("Chicken1special1", 1);
        PlayerPrefs.SetInt("Chicken2special1", 1);
        PlayerPrefs.SetInt("Chicken3special1", 1);

        PlayerPrefs.SetInt("Chicken1special2", 1);
        PlayerPrefs.SetInt("Chicken2special2", 1);
        PlayerPrefs.SetInt("Chicken3special2", 1);

        PlayerPrefs.SetInt("Chicken1special3", 1);
        PlayerPrefs.SetInt("Chicken2special3", 1);
        PlayerPrefs.SetInt("Chicken3special3", 1);

        // Load scene
        SceneManager.LoadScene("Intro World");
    }

}
