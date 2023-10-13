using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAlive : Node
{

    private Unit unit;
    
    public IsAlive(Unit a)
    {
        unit = a;
    }


    public override NodeState Evaluate()
    {
        if (unit.currentHP == 0)
        {
            Debug.Log("-IsAlive- says: " + unit.unitName + " is dead");
            state = NodeState.FAILURE;
            return state;
        }


        Debug.Log("-IsAlive- says: " + unit.unitName + " is alive");
        state = NodeState.SUCCESS;
        return state;
    }

}
