using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChickenAlive : Node
{

    private Unit playerUnit;

    public CheckChickenAlive(Unit pu)
    {
        playerUnit = pu;
    }

    public override NodeState Evaluate()
    {
        if (playerUnit.currentHP > 0)
        {
            Debug.Log(playerUnit + "IS ALIVE");
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }

}
