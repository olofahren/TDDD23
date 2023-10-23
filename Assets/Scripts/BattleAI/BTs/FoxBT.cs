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
                new CheckHPIsGreaterThanInt(enemy, enemy.maxHP/3),
                new TaskHeal(enemy, enemy.specialSkill2)
            }),

        });

        return root;
    }
}
