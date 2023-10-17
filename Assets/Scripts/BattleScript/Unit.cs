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

    public void setUnit(int lvl, int dmg, int mHP, int cHP, int def, int spe, int special1, int special2, int special3, int maxEXP, float cEXP)
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
        maxExp = maxEXP;
        currentExp = cEXP;
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

    public void Heal(int amount)
    {
        currentHP += amount; 

        if(currentHP > maxHP )
        {
            currentHP = maxHP;
        }
    }
    public void SetEXP(float exp)
    {
        currentExp += exp;

        if (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            unitLevel += 1;
            maxExp += 10;
            PlayerPrefs.SetInt("Chicken" + unitNr + "Lvl", unitLevel);
            PlayerPrefs.SetInt("Chicken" + unitNr + "maxEXP", maxExp);
        }

        PlayerPrefs.SetFloat("Chicken" + unitNr + "cEXP", currentExp);
        Debug.Log("-Unit- says: " + unitName + "-> lvl: " + unitLevel + ", cEXP: " + currentExp + ", maxEXP: " + maxExp);
    }


}
