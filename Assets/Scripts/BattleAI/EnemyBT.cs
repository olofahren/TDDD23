using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class EnemyBT : Tree
{
    protected override Node SetupTree()
    {
        Node root = new TaskAttack(enemy, player1);

        return root;
    }

}
