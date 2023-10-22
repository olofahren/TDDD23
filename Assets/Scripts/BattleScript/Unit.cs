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
    public int maxOfSpecialAttacks;
    public int noOfHeals;
    public int maxOfHeals;

    //public GameObject battleMenu;
   // public Boolean showBattleMenu;

    public string unitType;
    public int unitNr;

    //public BattleHud battleHud;

    // Enemny unit type
    public string enemyUnit;

    // EXP
    public int maxExp;
    public float currentExp;

    public void SetUnit(int lvl, int dmg, int mHP, int cHP, int def, int spe, 
        int special1, int special2, int special3, int maxEXP, float cEXP, 
        int nrSpA, int nrHeal, int maxNrSpA, int maxNrHeal)
    {
        Debug.Log("-Unit>SetUnit()- says: unit " + unitName + " has been set up.");
        unitLevel = lvl;
        damage = dmg;
        maxHP = mHP;
        currentHP = cHP;
        defense = def;  
        speed = spe;
        specialSkill1 = special1;
        specialSkill2 = special2;
        specialSkill3 = special3;
        maxExp = maxEXP;
        currentExp = cEXP;
        noOfSpecialAttacks = nrSpA;
        noOfHeals = nrHeal;
        maxOfSpecialAttacks = maxNrSpA;
        maxOfHeals = maxNrHeal;
        Debug.Log("-Unit>SetUnit()- says: unit " + unitName + " has maxHP: " + maxHP);
    }

    public Boolean CheckIfDead()
    {
        if (currentHP <= 0)
        {
            Debug.Log("-Unit>CheckIfDead()- says: unit " + unitName + " is dead.");
            return true;
        }
        else
        {
            Debug.Log("-Unit>CheckIfDead()- says: unit " + unitName + " is NOT dead, and has " + currentHP + " HP.");
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

    public bool Heal(int amount)
    {
        Debug.Log("-Unit>Heal(int amount)- says: " + unitName + " has noOfHeals " + noOfHeals + " left");
        if (noOfHeals >= 1)
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
    public void IncreaseStats(int increase)
    {
        int newHP = increase * 5;

        SetUnit(unitLevel, damage + increase, maxHP + newHP, currentHP + newHP, defense + increase, speed + increase, 
            specialSkill1 + increase, specialSkill2 + increase, specialSkill3 + increase, maxExp, currentExp, 
            maxOfSpecialAttacks + increase, maxOfHeals + increase, maxOfSpecialAttacks + increase, maxOfHeals + increase);
        Debug.Log("-Unit>IncreaseStats(int increase)- says: " + unitName + "maxHP has increased to: " + maxHP + ", speed has increased to: " + speed);
    }

    public void SetEXP(float exp)
    {
        currentExp += exp;

        if (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            unitLevel += 1;
            maxExp += 10;
            IncreaseStats(1); // The nr is the increase of the stats for each stat
            PlayerPrefs.SetInt("Chicken" + unitNr + "Lvl", unitLevel);
            PlayerPrefs.SetInt("Chicken" + unitNr + "maxEXP", maxExp);
        }

        PlayerPrefs.SetFloat("Chicken" + unitNr + "cEXP", currentExp);
        Debug.Log("-Unit>SetEXP(float exp)- says: " + unitName + "-> lvl: " + unitLevel + ", cEXP: " + currentExp + ", maxEXP: " + maxExp);
    }


}
