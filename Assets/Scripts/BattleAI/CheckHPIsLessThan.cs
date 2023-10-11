using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHPIsLessThan : Node
{

    private Unit unitA;
    private Unit unitB;

    public CheckHPIsLessThan(Unit a, Unit b)
    {
        unitA = a;
        unitB = b;
    }


    public override NodeState Evaluate()
    {
        if (unitA.currentHP <= unitB.currentHP)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }

}
