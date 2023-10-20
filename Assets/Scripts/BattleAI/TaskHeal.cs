using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using System;

public class TaskHeal : Node
{
    private Unit unit;
    private int healHp;
    private bool healState;
    public TaskHeal(Unit u, int hhp)
    {
        unit = u;
        healHp = hhp;
    }
    public override NodeState Evaluate()
    {

        healState = unit.Heal(healHp);

        if(healState == true )
        {
            Debug.Log("-TaskHeal- says: " + unit.unitName + " has healed " + healHp + " HP.");
            PlayerPrefs.SetInt("EnemycHP", unit.currentHP);
            PlayerPrefs.SetString("EnemyAttackType", unit.unitName + " feels renewed!");
            state = NodeState.SUCCESS; // State succeed??? 
            return state;
        }
        else
        {
            Debug.Log("-TaskHeal- says: " + unit.unitName + " has NOT healed, since it has no heals left.");
            PlayerPrefs.SetString("EnemyAttackType", unit.unitName + " cannot heal!\nIt has no heals left.");
            state = NodeState.FAILURE;
            return state;
        }



    }
}
