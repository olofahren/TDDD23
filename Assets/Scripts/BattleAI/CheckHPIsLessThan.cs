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
            Debug.Log("-CheckHPIsLessThan- says: " + unitA.unitName + " has HP " + unitA.currentHP + ", that is less or equal to the HP of " + unitB.unitName + " which has " + unitB.currentHP + "HP.");
            state = NodeState.SUCCESS;
            return state;
        }

        Debug.Log("-CheckHPIsLessThan- says: " + unitA.unitName + " has HP " + unitA.currentHP + ", that is greater to the HP of " + unitB.unitName + " which has " + unitB.currentHP + "HP.");
        state = NodeState.FAILURE;
        return state;
    }

}
