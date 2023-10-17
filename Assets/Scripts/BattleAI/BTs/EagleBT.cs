using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class EagleBT : Tree
{
    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
            //Subtree 1
            new Sequence(new List<Node>
            {
                new IsAlive(player1),
                new IsAlive(player2),
                new IsAlive(player3),
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckHPIsGreaterThanInt(enemy, enemy.maxHP*(2/3)),
                        new TaskAttack(enemy, player1),
                        new TaskAttack(enemy, player2),
                        new TaskAttack(enemy, player3)
                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckHPIsGreaterThanInt(enemy, enemy.maxHP*(1/2)),
                        new Selector(new List<Node>
                        {
                            new Sequence(new List<Node>
                            {
                                new CheckHPIsLessThan(player1, player2),
                                new CheckHPIsLessThan(player1, player3),
                                new TaskSpecialAttack(enemy, player1)
                            }),
                            new Sequence(new List<Node>
                            {
                                new CheckHPIsLessThan(player2, player3),
                                new TaskSpecialAttack(enemy, player2)
                            }),
                            new TaskSpecialAttack(enemy, player3)
                        })

                    }),
                    new Selector(new List<Node>
                    {
                         new Sequence(new List<Node>
                         {
                             new CheckHPIsLessThan(player1, player2),
                             new CheckHPIsLessThan(player1, player3),
                             new TaskAttack(enemy, player1)
                         }),
                         new Sequence(new List<Node>
                         {
                             new CheckHPIsLessThan(player2, player3),
                             new TaskAttack(enemy, player2)
                         }),
                         new TaskAttack(enemy, player3)
                    }),
                })
            })
            //Subtree 2
            //Subtree 3
            //Subtree 4
            //Subtree 5
            //Subtree 6
            //Subtree 7

        });;
        return root;
    }

}
