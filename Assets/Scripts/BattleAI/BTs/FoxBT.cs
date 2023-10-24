using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using BehaviourTree;

public class FoxBT : Tree
{
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                // If the fox speed is higher then any of the chickens
                // -> Instant kill the chickens
                new CheckSpeed(player1, enemy),
                new CheckSpeed(player2, enemy),
                new CheckSpeed(player3, enemy),
                new InstantKill(player1),
                new InstantKill(player2),
                new InstantKill(player3)
            }),
            new Sequence(new List<Node>
            {
                // If the HP is less then a 1/3 of its max HP it heals
                new CheckHP(enemy, (enemy.maxHP/3)),
                new TaskHeal(enemy, enemy.specialSkill1)
            }),
            new Selector(new List<Node>
            {
                // Attack the chickens in different ways depending on who's alive
                new Sequence(new List<Node>
                {
                    // All chickens are alive
                    new IsAlive(player1),
                    new IsAlive(player2),
                    new IsAlive(player3),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            // Special Attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/4*3)),
                            new TaskSpecialAttack(enemy, player1),
                            new TaskSpecialAttack(enemy, player2),
                            new TaskSpecialAttack(enemy, player3)
                        }),
                        new Sequence(new List<Node>
                        {   
                            // Normal attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/3*2)),
                            new TaskAttack(enemy, player1),
                            new TaskAttack(enemy, player2),
                            new TaskAttack(enemy, player3)
                        }),
                        new Selector(new List<Node>
                        {
                            // Find the chicken with the least HP
                            new Sequence(new List<Node>
                            {
                                // Chicken1 has the least HP
                                new CheckHPIsLessThan(player1, player2),
                                new CheckHPIsLessThan(player1, player3),
                                new TaskAttack(enemy, player1)
                            }),
                            new Sequence(new List<Node>
                            {
                                // Chicken2 has the least HP
                                new CheckHPIsLessThan(player2, player3),
                                new TaskAttack(enemy, player2)
                            }),
                            // Chickn3 has the least HP
                            new TaskAttack(enemy, player3)
                        })
                    })
                }),
                // Chicken1 and Chicken2 are alive
                new Sequence(new List<Node>
                {
                    new IsAlive(player1),
                    new IsAlive(player2),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            // Special Attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/4*3)),
                            new TaskSpecialAttack(enemy, player1),
                            new TaskSpecialAttack(enemy, player2),
                        }),
                        new Sequence(new List<Node>
                        {   
                            // Normal attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/3*2)),
                            new TaskAttack(enemy, player1),
                            new TaskAttack(enemy, player2),
                        }),
                        new Selector(new List<Node>
                        {
                            // Find the chicken with the least HP
                            new Sequence(new List<Node>
                            {
                                // Chicken1 has the least HP
                                new CheckHPIsLessThan(player1, player2),
                                new TaskAttack(enemy, player1)
                            }),
                            // Chickn2 has the least HP
                            new TaskAttack(enemy, player2)
                        })
                    })
                }),
                 // Chicken1 and Chicken3 are alive
                new Sequence(new List<Node>
                {
                    new IsAlive(player1),
                    new IsAlive(player3),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            // Special Attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/4*3)),
                            new TaskSpecialAttack(enemy, player1),
                            new TaskSpecialAttack(enemy, player3),
                        }),
                        new Sequence(new List<Node>
                        {   
                            // Normal attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/3*2)),
                            new TaskAttack(enemy, player1),
                            new TaskAttack(enemy, player3),
                        }),
                        new Selector(new List<Node>
                        {
                            // Find the chicken with the least HP
                            new Sequence(new List<Node>
                            {
                                // Chicken1 has the least HP
                                new CheckHPIsLessThan(player1, player3),
                                new TaskAttack(enemy, player1)
                            }),
                            // Chickn3 has the least HP
                            new TaskAttack(enemy, player3)
                        })
                    })
                }),
                // Chicken2 and Chicken3 are alive
                new Sequence(new List<Node>
                {
                    new IsAlive(player2),
                    new IsAlive(player3),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            // Special Attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/4*3)),
                            new TaskSpecialAttack(enemy, player2),
                            new TaskSpecialAttack(enemy, player3),
                        }),
                        new Sequence(new List<Node>
                        {   
                            // Normal attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/3*2)),
                            new TaskAttack(enemy, player2),
                            new TaskAttack(enemy, player3),
                        }),
                        new Selector(new List<Node>
                        {
                            // Find the chicken with the least HP
                            new Sequence(new List<Node>
                            {
                                // Chicken2 has the least HP
                                new CheckHPIsLessThan(player2, player3),
                                new TaskAttack(enemy, player2)
                            }),
                            // Chickn3 has the least HP
                            new TaskAttack(enemy, player3)
                        })
                    })
                }),
                // Chicken1 is alive
                new Sequence(new List<Node>
                {
                    new IsAlive(player1),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            // Special Attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/2)),
                            new TaskSpecialAttack(enemy, player1),
                        }),
                        new TaskAttack(enemy, player1),

                    })
                }),
                // Chicken2 is alive
                new Sequence(new List<Node>
                {
                    new IsAlive(player2),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            // Special Attack on all chickens
                            new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/2)),
                            new TaskSpecialAttack(enemy, player2),
                        }),
                        new TaskAttack(enemy, player2),

                    })
                }),
                // Chicken3 is alive
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        // Special Attack on all chickens
                        new CheckHPIsGreaterThanInt(enemy, (enemy.maxHP/2)),
                        new TaskSpecialAttack(enemy, player3),
                    }),
                    new TaskAttack(enemy, player3),
                })
                
            })

        });

        return root;
    }
}
