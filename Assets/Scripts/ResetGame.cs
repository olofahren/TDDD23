using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private List<int> collectedEggs = Enumerable.Repeat(0, 21).ToList();
    private List<int> completedBattles = Enumerable.Repeat(0, 26).ToList();

    // To be set the player prefs
    public Unit player1;
    public Unit player2;
    public Unit player3;

    public BattleFunctions battleFunctions;

    public void resetGame()
    {
        collectedEggs.ForEach(egg => Debug.Log(egg));
        PlayerPrefsExtra.SetList("collectedEggs", collectedEggs);
        PlayerPrefsExtra.SetList("completedBattles", completedBattles);

        PlayerPrefs.SetFloat("PlayerX", 0);
        PlayerPrefs.SetFloat("PlayerY", 0);
        PlayerPrefs.SetFloat("PlayerZ", 0);

        // Player Unit Set Stats
        battleFunctions.AssignStats(player1.unitNr, player1.unitLevel, player1.damage,
            player1.maxHP, player1.maxHP, player1.defense, player1.speed,
            player1.specialSkill1, player1.specialSkill2, player1.specialSkill3, 
            player1.maxExp, player1.currentExp, player1.noOfSpecialAttacks, player1.noOfHeals,
            player1.maxOfSpecialAttacks, player1.maxOfHeals);

        battleFunctions.AssignStats(player2.unitNr, player2.unitLevel, player2.damage,
            player2.maxHP, player2.maxHP, player2.defense, player2.speed,
            player2.specialSkill1, player2.specialSkill2, player2.specialSkill3, 
            player2.maxExp, player2.currentExp, player2.noOfSpecialAttacks, player2.noOfHeals,
            player2.maxOfSpecialAttacks, player2.maxOfHeals);

        battleFunctions.AssignStats(player3.unitNr, player3.unitLevel, player3.damage,
            player3.maxHP, player3.maxHP, player3.defense, player3.speed,
            player3.specialSkill1, player3.specialSkill2, player3.specialSkill3, 
            player3.maxExp, player3.currentExp, player3.noOfSpecialAttacks, player3.noOfHeals,
            player3.maxOfSpecialAttacks, player3.maxOfHeals);


        // Reload the scene
        SceneManager.LoadScene("Main World");

    }
}
