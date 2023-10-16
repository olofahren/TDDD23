using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHPIsGreaterThanInt : Node
{

    private Unit unit;
    private int HPLevel;

    public CheckHPIsGreaterThanInt(Unit u, int hpl)
    {
        unit = u;
        HPLevel = hpl;
    }


    public override NodeState Evaluate()
    {
        if (unit.currentHP >= HPLevel)
        {
            Debug.Log("-CheckHPIsGreaterThanInt- says: " + unit.unitName + " has HP " + unit.currentHP + " that is greater or equal to " + HPLevel);
            state = NodeState.SUCCESS;
            return state;
        }
        Debug.Log("-CheckHPIsGreaterThanInt- says: " + unit.unitName + " has HP " + unit.currentHP + "that is less than " + HPLevel);
        state = NodeState.FAILURE;
        return state;
    }

}
