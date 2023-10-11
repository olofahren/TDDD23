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

        // Player take damage
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage + enemyUnit.specialSkill2);
        Debug.Log("Enemy special attacked " + playerUnit.unitName);
        String tempUnit = "Chicken" + playerUnit.unitNr.ToString() + "cHP";

        //Enemy take damage (Special attack damages the unit using it also)
        enemyUnit.TakeDamage(enemyUnit.specialSkill2);


        // Update the current HP so it can update the UI
        PlayerPrefs.SetInt(tempUnit, playerUnit.currentHP);
        PlayerPrefs.SetInt("EnemycHP", enemyUnit.currentHP);


        state = NodeState.SUCCESS; // State succeed??? 
        return state;
    }
}
