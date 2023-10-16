using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int defense;
    public int speed;

    public int specialSkill1; 
    public int specialSkill2;
    public int specialSkill3;

    //variables to keep track of special attacks and heals
    public int noOfSpecialAttacks;
    public int noOfHeals;

    //public GameObject battleMenu;
   // public Boolean showBattleMenu;

    public string unitType;
    public int unitNr;

    //public BattleHud battleHud;

    // Enemny unit type
    public string enemyUnit; 


    public void setUnit(int lvl, int dmg, int mHP, int cHP, int def, int spe, int special1, int special2, int special3)
    {
        Debug.Log("-Unit- says: unit " + unitName + " has been set up.");
        unitLevel = lvl;
        damage = dmg;
        maxHP = mHP;
        currentHP = cHP;
        defense = def;  
        speed = spe;
        specialSkill1 = special1;
        specialSkill2 = special2;
        specialSkill3 = special3;
    }

    private Boolean CheckIfDead()
    {
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean TakeDamage(int dmg)
    {

        currentHP -= dmg;

        if (currentHP < 0)
        {
            currentHP = 0;
        }
        PlayerPrefs.SetInt("Chicken"+ unitNr +"cHP", currentHP);

        Debug.Log("-Unit>TakeDamage(int dmg)- says: " + unitName + " has taken " + dmg + " damage, and now has HP " + currentHP + ".");

        return CheckIfDead();
    }

    public Boolean TakeDamage(int dmg, int special)
    {
        currentHP -= (dmg + special);

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        Debug.Log("-Unit>TakeDamage(int dmg, int special)- says: " + unitName + " has taken " + dmg + " damage, and now has HP " + currentHP + ".");

        return CheckIfDead();
    }

    public Boolean BlockDamage(int dmg, int def)
    {
        int totDmg = dmg - def;

        if (totDmg < 0)
        {
            totDmg = 0;
        }

        Debug.Log(totDmg);

        currentHP -= totDmg;

        return CheckIfDead();
    }

    public bool Heal(int amount)
    {
        if(noOfHeals >= 1)
        {
            currentHP += amount;
            noOfHeals--;

            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
            return true;
        }
        return false;
        
    }

}
