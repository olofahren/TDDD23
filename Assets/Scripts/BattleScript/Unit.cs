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

    public int deffense;
    public int speed;

    public int specialSill1; 
    public int specialSill2;
    public int specialSill3;

    public GameObject battleMenu;
    public Boolean showBattleMenu;

    public string unitType;
    public int unitNr;

    public BattleHud battleHud;

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

}
