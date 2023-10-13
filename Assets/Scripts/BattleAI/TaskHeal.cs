using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using System;

public class TaskHeal : Node
{
    private Unit unit;
    private int healHp;
    public TaskHeal(Unit u, int hhp)
    {
        unit = u;
        healHp = hhp;
    }
    public override NodeState Evaluate()
    {

        unit.Heal(healHp);
        Debug.Log("-TaskHeal- says: " + unit.unitName + " has healed " + healHp + " HP.");

        PlayerPrefs.SetInt("EnemycHP", unit.currentHP);

        state = NodeState.SUCCESS; // State succeed??? 
        return state;
    }
}
