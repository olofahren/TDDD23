using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFunctions : MonoBehaviour
{
    public BattleSystem battleSystem;
  
    // Set global stats variables to be used between battles
    public void assignStats(int unitNr, int lvl, int dmg, int mHP, int cHP, int def, int spe, int spec1, int spec2, int spec3)
    {
        PlayerPrefs.SetInt("Chicken" + unitNr + "Lvl", lvl);
        PlayerPrefs.SetInt("Chicken" + unitNr + "dmg", dmg);
        PlayerPrefs.SetInt("Chicken" + unitNr + "maxHP", mHP);
        PlayerPrefs.SetInt("Chicken" + unitNr + "cHP", cHP);
        PlayerPrefs.SetInt("Chicken" + unitNr + "def", def);
        PlayerPrefs.SetInt("Chicken" + unitNr + "speed", spe);
        PlayerPrefs.SetInt("Chicken" + unitNr + "special1", spec1);
        PlayerPrefs.SetInt("Chicken" + unitNr + "special2", spec2);
        PlayerPrefs.SetInt("Chicken" + unitNr + "special3", spec3);

        Debug.Log("Chicken" + unitNr + "cHP: " + cHP);
    }

    // Function used when flee button i spressed
    public IEnumerator PlayerFlee()
    { 
        // Switch state
        battleSystem.state = BattleState.WAITING;
        battleSystem.state = BattleState.FLEE;

        battleSystem.EndBattle();
        yield return new WaitForSeconds(2f);
    }

    // Function used when player uses block button
    public IEnumerator PlayerBlock()
    {
        int unitNr = battleSystem.allUnit[battleSystem.turnIndex].unitNr;

        if ( unitNr == 1)
        {
            battleSystem.blockingPlayer1 = true;

        }
        else if (unitNr == 2)
        {
            battleSystem.blockingPlayer2 = true;
        }
        else // Unit 3
        {
            battleSystem.blockingPlayer3 = true;
        }


        battleSystem.state = BattleState.WAITING; // Prevent button spamming

        yield return new WaitForSeconds(2f);

        // Get next state, check who's turn it is
        battleSystem.getState(battleSystem.allUnit[battleSystem.turnIndex].unitType);

    }

    // Function used then player presses heal button or enemy have heal as special skill
    public IEnumerator PlayerHeal()
    {
        Unit currentUnit = battleSystem.allUnit[battleSystem.turnIndex];

        currentUnit.Heal(5);

        if (currentUnit.unitType == "enemy")
        {
            battleSystem.enemyHUD.SetHP(currentUnit.currentHP);
        }
        else if (currentUnit.unitNr == 1)
        {
            battleSystem.playerHUD1.SetHP(currentUnit.currentHP);
        }
        else if (currentUnit.unitNr == 2)
        {
            battleSystem.playerHUD2.SetHP(currentUnit.currentHP);
        }
        else if(currentUnit.unitNr == 3)// Unit 3
        {
            battleSystem.playerHUD2.SetHP(currentUnit.currentHP);
        }

        battleSystem.state = BattleState.WAITING; // Prevent button spamming

        battleSystem.dialogueText.text = currentUnit.unitName + " feel renewed!";

        yield return new WaitForSeconds(2f);

       // Get state, check who's turn it is
        battleSystem.getState(currentUnit.unitType);
    }

    public IEnumerator PlayerAttack()
    {
        // Damage the enemy
        //bool isDead = enemyUnit.TakeDamage(playerUnit1.damage);
        bool isDead = false;
        Unit currentUnit = battleSystem.allUnit[battleSystem.turnIndex];

        if (currentUnit.unitNr == 1)
        {
            isDead = battleSystem.enemyUnit.TakeDamage(battleSystem.playerUnit1.damage);

        }
        else if (currentUnit.unitNr == 2)
        {
            isDead = battleSystem.enemyUnit.TakeDamage(battleSystem.playerUnit2.damage);
        }
        else // Unit 3
        {
            isDead = battleSystem.enemyUnit.TakeDamage(battleSystem.playerUnit3.damage);
        }

        battleSystem.enemyHUD.SetHP(battleSystem.enemyUnit.currentHP); // Change later depending on if more then one enemy unit
        battleSystem.state = BattleState.WAITING; // Prevent button spamming

        battleSystem.dialogueText.text = "The attack is succesfull!";

        yield return new WaitForSeconds(2f);

        // Check if enemy is dead 
        if (isDead)
        {
            // End the battle
            battleSystem.state = BattleState.WON;
            battleSystem.EndBattle();
        }
        else
        {
            battleSystem.getState(currentUnit.unitType);
        }
        // Change state based on what has happened

    }

}
