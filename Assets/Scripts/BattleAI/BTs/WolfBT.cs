using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class WolfBT : Tree
{
    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
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
                            new Selector(new List<Node>
                            {
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player1, enemy.damage),
                                    new TaskAttack(enemy, player1)
                                }),
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player1, enemy.damage + enemy.specialSkill2),
                                    //Check if can do special attack
                                    new TaskSpecialAttack(enemy, player1)
                                }),
                                new TaskAttack(enemy, player1)
                            })
                        }),
                        new Sequence(new List<Node>
                        {
                            new CheckHPIsLessThan(player2, player3),
                            new Selector(new List<Node>
                            {
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player2, enemy.damage),
                                    new TaskAttack(enemy, player2)
                                }),
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player2, enemy.damage + enemy.specialSkill2),
                                    //Check if can do special attack
                                    new TaskSpecialAttack(enemy, player2)
                                }),
                                new TaskAttack(enemy, player2)
                            })
                        }),
                        new Selector (new List<Node>
                        {
                            new Sequence (new List<Node>
                            {
                                new CheckHP(player3, enemy.damage),
                                new TaskAttack(enemy, player3)
                            }),
                            new Sequence (new List<Node>
                            {
                                new CheckHP(player3, enemy.damage + enemy.specialSkill2),
                                //Check if can do special attack
                                new TaskSpecialAttack(enemy, player3)
                            }),
                            new TaskAttack(enemy, player3)
                        })
                    })
                }),
                //SUB TREE 2
                new Sequence(new List<Node>
                {
                    new IsAlive(player1),
                    new IsAlive(player2),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new CheckHPIsLessThan(player1, player2),
                            new Selector (new List<Node>
                            {
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player1, enemy.damage),
                                    new TaskAttack(enemy, player1)
                                }),
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player1, enemy.damage + enemy.specialSkill2),
                                    //Check if can do special attack
                                    new TaskSpecialAttack (enemy, player1)

                                }),
                                new TaskAttack(enemy, player1)

                            })
                        }),
                        new Selector(new List<Node>
                        {
                            new Sequence(new List<Node>
                            {
                                new CheckHP(player2, enemy.damage),
                                new TaskAttack(enemy, player2)
                            }),
                            new Sequence(new List<Node>
                            {
                                new CheckHP(player2, enemy.damage + enemy.specialSkill2),
                                //Check if can do special attack
                                new TaskSpecialAttack (enemy, player2)
                            }),
                            new TaskAttack(enemy, player2)
                        })
                    })
                }),
                //SUB TREE 3
                new Sequence(new List<Node>
                {
                    new IsAlive(player1),
                    new IsAlive(player3),
                    new Selector(new List<Node>
                    {
                        new Sequence (new List<Node>
                        {
                            new CheckHPIsLessThan(player1, player3),
                            new Selector (new List<Node>
                            {
                                new Sequence(new List<Node>
                            {
                                new CheckHP(player1, enemy.damage),
                                new TaskAttack(enemy, player1)
                            }),
                            new Sequence(new List<Node>
                            {
                                new CheckHP(player1, enemy.damage + enemy.specialSkill2),
                                //Check if can do special attack
                                new TaskSpecialAttack (enemy, player1)
                            }),
                            new TaskAttack(enemy, player1)
                            })
                        }),
                        new Selector (new List<Node>
                        {
                            new Selector (new List<Node>
                            {
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player3, enemy.damage),
                                    new TaskAttack(enemy, player3)
                                }),
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player3, enemy.damage + enemy.specialSkill2),
                                    //Check if can do special attack
                                    new TaskSpecialAttack (enemy, player3)

                                }),
                                new TaskAttack(enemy, player3)

                            })
                        })
                    })
                }),
                //SUBTREE 4
                new Sequence (new List<Node>
                {
                    new IsAlive(player2),
                    new IsAlive(player3),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new CheckHPIsLessThan(player2, player3),
                            new Selector (new List<Node>
                            {
                                new Sequence(new List<Node>
                            {
                                new CheckHP(player2, enemy.damage),
                                new TaskAttack(enemy, player2)
                            }),
                            new Sequence(new List<Node>
                            {
                                new CheckHP(player2, enemy.damage + enemy.specialSkill2),
                                //Check if can do special attack
                                new TaskSpecialAttack (enemy, player2)
                            }),
                            new TaskAttack(enemy, player2)
                            })
                        }),
                        new Selector(new List<Node>
                        {
                            new Selector (new List<Node>
                            {
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player3, enemy.damage),
                                    new TaskAttack(enemy, player3)
                                }),
                                new Sequence(new List<Node>
                                {
                                    new CheckHP(player3, enemy.damage + enemy.specialSkill2),
                                    //Check if can do special attack
                                    new TaskSpecialAttack (enemy, player3)

                                }),
                                new TaskAttack(enemy, player3)

                            })
                        })
                    })
                }),
                //SUB TREE 5
                new Sequence(new List<Node>
                {
                    new IsAlive(player1),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new CheckHP(player1, enemy.damage),
                            new TaskAttack(enemy, player1)
                        }),
                        new Sequence(new List<Node>
                        {
                            new CheckHP(player1, enemy.damage + enemy.specialSkill2),
                            //Check if can do special attack
                            new TaskSpecialAttack (enemy, player1)
                        }),
                        new TaskAttack(enemy, player1)
                    })
                }),
                //SUB TREE 6
                new Sequence(new List<Node>
                {
                    new IsAlive(player2),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new CheckHP(player2, enemy.damage),
                            new TaskAttack(enemy, player2)
                        }),
                        new Sequence(new List<Node>
                        {
                            new CheckHP(player2, enemy.damage + enemy.specialSkill2),
                            //Check if can do special attack
                            new TaskSpecialAttack (enemy, player2)
                        }),
                        new TaskAttack(enemy, player2)
                    })
                }),
                //SUB TREE 7
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckHP(player3, enemy.damage),
                        new TaskAttack(enemy, player3)
                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckHP(player3, enemy.damage + enemy.specialSkill2),
                        //Check if can do special attack
                        new TaskSpecialAttack (enemy, player3)
                    }),
                    new TaskAttack(enemy, player3)
                 })

        });
        return root;
    }

}
