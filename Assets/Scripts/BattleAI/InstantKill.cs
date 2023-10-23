using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using System.Numerics;
using System;
public class InstantKill : Node
{ 
    private Unit playerUnit;
    
    public InstantKill(Unit pu)
    {
        playerUnit = pu;
    }
    public override NodeState Evaluate()
    {
        // Player take max damage (full HP) and is instantly killed
        bool isDead = playerUnit.TakeDamage(playerUnit.maxHP);

        String tempUnit = "Chicken" + playerUnit.unitNr + "cHP";

        // Update the current HP so it can update the UI
        PlayerPrefs.SetInt(tempUnit, playerUnit.currentHP);
        Debug.Log("-InstantKilled- says: " + playerUnit.unitName + " has been instantly killed!");
        PlayerPrefs.SetString("EnemyAttackType", "The enemy has attacked the chickens!");

        state = NodeState.SUCCESS;
        return state;
    }


}
