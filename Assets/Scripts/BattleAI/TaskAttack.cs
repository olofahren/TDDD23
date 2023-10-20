using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using System;

public class TaskAttack : Node
{
    private Unit enemyUnit;
    private Unit playerUnit;
    public TaskAttack(Unit eu, Unit pu)
    { 
        enemyUnit = eu;
        playerUnit = pu;
    }
    public override NodeState Evaluate()
    {
       
        // Player take damage
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        Debug.Log("-TaskAttack- says: " + enemyUnit.unitName + " has attacked " + playerUnit.unitName + " and dealt " + enemyUnit.damage);
        String tempUnit = "Chicken" + playerUnit.unitNr.ToString() + "cHP";

        // Update the current HP so it can update the UI
        PlayerPrefs.SetInt(tempUnit, playerUnit.currentHP);
        PlayerPrefs.SetString("EnemyAttackType", enemyUnit.unitName + " attacked the chickens!");
       
        state = NodeState.SUCCESS; // State succeed??? 
        return state;
    }
}
