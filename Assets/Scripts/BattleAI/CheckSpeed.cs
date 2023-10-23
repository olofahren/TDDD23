using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckSpeed :Node
{
    private Unit unitA;
    private Unit unitB;

    public CheckSpeed(Unit a, Unit b)
    {
        unitA = a;
        unitB = b;
    }

    public override NodeState Evaluate()
    {
        if(unitA.speed <= unitB.speed)
        {
            Debug.Log("-CheckSpeed- says: " + unitA.unitName + " has speed " + unitA.speed + ", that is less or equal to the spped of " + unitB.unitName + " which has speed " + unitB.speed);
            state = NodeState.SUCCESS;
            return state;
        }

        Debug.Log("-CheckSpeed- says: " + unitA.unitName + " has speed " + unitA.speed + ", that is greater to the spped of " + unitB.unitName + " which has speed " + unitB.speed);
        state = NodeState.FAILURE;
        return state;
    }
}
