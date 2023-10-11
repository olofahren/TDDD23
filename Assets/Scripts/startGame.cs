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
        /*battleFunctions.AssignStats(player1.unitNr, player1.unitLevel, player1.damage,
            player1.maxHP, player1.maxHP, player1.defense, player1.speed,
            player1.specialSill1, player1.specialSill2, player1.specialSill3);

        battleFunctions.AssignStats(player2.unitNr, player2.unitLevel, player2.damage,
            player2.maxHP, player2.maxHP, player2.defense, player2.speed,
            player2.specialSill1, player2.specialSill2, player2.specialSill3);

        battleFunctions.AssignStats(player3.unitNr, player3.unitLevel, player3.damage,
            player3.maxHP, player3.maxHP, player3.defense, player3.speed,
            player3.specialSill1, player3.specialSill2, player3.specialSill3);*/

        // AssignStats(unitNr, lvl, damage, maxHP, currentHP, defense, speed, specialSkill1, spcialSkill2, speicalSkill3)
        battleFunctions.AssignStats(1, 1, 1, 10, 10, 2, 1, 1, 1, 1);
        battleFunctions.AssignStats(2, 1, 2, 8, 8, 1, 3, 1, 1, 1);
        battleFunctions.AssignStats(3, 1, 1, 12, 12, 1, 1, 1, 1, 1);

        // Load scene
        SceneManager.LoadScene("Intro World");
    }

}
