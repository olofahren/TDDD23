using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    private List<int> collectedEggs = Enumerable.Repeat(1, 21).ToList();
    private List<int> completedBattles= Enumerable.Repeat(0, 20).ToList();

    // To be set the player prefs
    public Unit player1;
    public Unit player2;
    public Unit player3;

    public BattleFunctions battleFunctions;

    public void StartGame()
    {
        //collectedEggs.ForEach(egg => Debug.Log(egg));
        collectedEggs[0] = 0;
        PlayerPrefs.SetFloat("PlayerX", 0);
        PlayerPrefs.SetFloat("PlayerY", 0);
        PlayerPrefs.SetFloat("PlayerZ", 0);
        PlayerPrefsExtra.SetList("collectedEggs", collectedEggs);
        PlayerPrefsExtra.SetList("completedBattles", completedBattles);


        // Player Unit Set Stats
        // AssignStats(unitNr, lvl, damage, maxHP, currentHP, defense, speed, specialSkill1, spcialSkill2, speicalSkill3,
        // maxEXP, currentEXP, noOfHeals, noOfSpA, maxNrHeals, maxNrSpA )
        battleFunctions.AssignStats(1, 1, 1, 10, 10, 2, 1, 1, 1, 1, 10, 0.0f, 2, 2, 2, 2);
        battleFunctions.AssignStats(2, 1, 2, 8, 8, 1, 3, 1, 1, 1, 10, 0.0f, 2 ,2, 2, 2);
        battleFunctions.AssignStats(3, 1, 1, 12, 12, 1, 1, 1, 1, 1, 10, 0.0f, 2, 2, 2, 2);

        // Load scene
        SceneManager.LoadScene("Intro World");
    }

}
