using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLevel : Node
{
    private Unit unitA;
    private int thresHoldLevel;

    public CheckLevel(Unit a, int lvl)
    {
        unitA = a;
        thresHoldLevel = lvl;
    }

    public override NodeState Evaluate()
    {
        if (unitA.unitLevel <= thresHoldLevel)
        {
            Debug.Log("-CheckLevel- says: " + unitA.unitName + " has lower level then thresholdlevel " + thresHoldLevel );
            state = NodeState.SUCCESS;
            return state;
        }

        Debug.Log("-CheckLevel- says: " + unitA.unitName + " has higher level then thresholdlevel " + thresHoldLevel);
        state = NodeState.FAILURE;
        return state;
    }
}
