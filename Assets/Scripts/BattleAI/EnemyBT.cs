using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class EnemyBT : Tree
{
    protected override Node SetupTree()
    {
        //Node root = new TaskAttack(enemy, player1);

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckChickenAlive(player1),
                new TaskAttack(enemy, player1),
            }),
            new Sequence(new List<Node>
            {
                new CheckChickenAlive(player2),
                new TaskAttack(enemy, player2),
            }),
            new Sequence(new List<Node>
            {
                new CheckChickenAlive(player3),
                new TaskAttack(enemy, player3),
            }),
            //new task end battle
        });
        

        return root;
    }

}
