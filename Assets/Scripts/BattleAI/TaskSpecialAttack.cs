using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using System;

public class TaskSpecialAttack : Node
{
    private Unit enemyUnit;
    private Unit playerUnit;
    public TaskSpecialAttack(Unit eu, Unit pu)
    {
        enemyUnit = eu;
        playerUnit = pu;
    }
    public override NodeState Evaluate()
    {
        if(enemyUnit.noOfSpecialAttacks >= 1)
        {
            playerUnit.GetComponent<BattleAnimation>().PlayDamageAnimation(); // Play damage animation for the chicken that is attacked
            // Player take damage
            bool isDead = playerUnit.TakeDamage(enemyUnit.damage + enemyUnit.specialSkill2);
            Debug.Log("-TaskSpecialAttack- says: " + enemyUnit.unitName + " has special attacked " + playerUnit.unitName + " and dealt " + enemyUnit.damage + enemyUnit.specialSkill2);
            String tempUnit = "Chicken" + playerUnit.unitNr.ToString() + "cHP";

            //Enemy take damage (Special attack damages the unit using it also)
            enemyUnit.TakeDamage(enemyUnit.specialSkill2);
            // Decreases the amount of special attacks a unit has
            enemyUnit.noOfSpecialAttacks--;
            Debug.Log("-TaskSpecialAttack- says: " + enemyUnit.unitName + " has noOfSpecialAttacks " + enemyUnit.noOfSpecialAttacks + " left.");


            // Update the current HP so it can update the UI
            PlayerPrefs.SetInt(tempUnit, playerUnit.currentHP);
            PlayerPrefs.SetInt("EnemycHP", enemyUnit.currentHP);

            PlayerPrefs.SetString("EnemyAttackType", enemyUnit.unitName + " attacked the chickens \nwith a special attack!");

            state = NodeState.SUCCESS; // State succeed??? 
            return state;
        }
        else
        {
            Debug.Log("-TaskSpecialAttack- says: " + enemyUnit.unitName + " tried to special attacked " + playerUnit.unitName + ", but failed since it has no special attacks left.");
            PlayerPrefs.SetString("EnemyAttackType", enemyUnit.unitName + " tried to do a special attack but failed...");
            state = NodeState.FAILURE; 
            return state;
        }

    }
}
