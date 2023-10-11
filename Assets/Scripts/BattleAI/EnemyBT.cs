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
                new CheckHP(enemy, (enemy.maxHP/2)),
                new TaskHeal(enemy, enemy.specialSkill1),
            }),
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
                            new TaskAttack(enemy, player1),
                        }),  
                        new Sequence(new List<Node>
                        {
                            new CheckHP(player1, enemy.damage + enemy.specialSkill2),
                            //Check if enemy has special attacks left
                            new TaskSpecialAttack(enemy, player1),
                        }),
                        new TaskAttack(enemy, player1)
                    }),
                }),
                new Sequence(new List<Node>
                {
                    new CheckHPIsLessThan(player2, player3),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new CheckHP(player2, enemy.damage),
                            new TaskAttack(enemy, player2),
                        }),
                        new Sequence(new List<Node>
                        {
                            new CheckHP(player2, enemy.damage +  enemy.specialSkill2),
                            //Check if enemy has special attacks left
                            new TaskSpecialAttack(enemy, player2),
                        }),
                        new TaskAttack(enemy, player2)
                    })
                }),
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckHP(player3, enemy.damage),
                        new TaskAttack(enemy, player3),
                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckHP(player3, enemy.damage + enemy.specialSkill2),
                        //Check if enemy has special attacks left
                        new TaskSpecialAttack(enemy, player3),
                    }),
                    new TaskAttack(enemy, player3)

                }),
            }),
        });;;;;
        

        return root;
    }

}
