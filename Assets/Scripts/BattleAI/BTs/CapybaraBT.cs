using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class CapybaraBT : Tree
{
    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckHP(enemy, enemy.maxHP/3),
                    new TaskHeal(enemy, enemy.specialSkill1)
                })
            }),
            new Selector(new List<Node>
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
                    })
                }),
                //Subtree 2
                new Sequence(new List<Node>
                {
                    new IsAlive(player1),
                    new IsAlive(player2),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new CheckHPIsLessThan(player1, player2),
                            new TaskAttack(enemy, player1)
                        }),
                        new TaskAttack(enemy, player2)
                    })
                }),
                //Subtree 3
                new Sequence(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new IsAlive(player1),
                        new IsAlive(player3),
                        new Selector(new List<Node>
                        {
                            new Sequence(new List<Node>
                            {
                                new CheckHPIsLessThan(player1, player3),
                                new TaskAttack(enemy, player1)
                            }),
                            new TaskAttack(enemy, player3)
                        })
                    })
                }),
                //Subtree 4
                new Sequence(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new IsAlive(player2),
                        new IsAlive(player3),
                        new Selector(new List<Node>
                        {
                            new Sequence(new List<Node>
                            {
                                new CheckHPIsLessThan(player2, player3),
                                new TaskAttack(enemy, player2)
                            }),
                            new TaskAttack(enemy, player3)
                        })
                    })
                }),
                //Subtree 5
                new Sequence(new List<Node>
                {
                    new IsAlive(player1),
                    new TaskAttack(enemy, player1)
                }),
                new Sequence(new List<Node>
                {
                    new IsAlive(player2),
                    new TaskAttack(enemy, player2)
                }),
                new TaskAttack(enemy, player3)
            })
        }) ;
        return root;
    }

}
