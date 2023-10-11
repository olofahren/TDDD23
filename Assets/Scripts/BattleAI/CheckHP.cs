using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHP : Node
{

    private Unit unit;
    private int HPLevel;

    public CheckHP(Unit u, int hpl)
    {
        unit = u;
        HPLevel = hpl;
    }


    public override NodeState Evaluate()
    {
        if (unit.currentHP <= HPLevel)
        {
            Debug.Log("CheckHP SUCCESS");
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }

}
