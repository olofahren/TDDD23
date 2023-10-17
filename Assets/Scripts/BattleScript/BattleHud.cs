using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // UI Mesh
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEngine.UI.CanvasScaler;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;
    public Slider expSlider;

    // Set the values for the HUD
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        if (unit.unitType == "player")
        {
            expSlider.maxValue = unit.maxExp;
            expSlider.value = unit.currentExp;
            Debug.Log("-BattleHud- says: " + unit.unitName + "-> slider.value: " + unit.currentExp + ", slider.maxValue: " +  unit.maxExp);
        }
    }

    // Update the HP slider
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetEXP(float exp)
    {
        expSlider.value = exp;
    }

    public void SetLVL(int lvl)
    {
        levelText.text = "Lvl " + lvl;
    }
}
