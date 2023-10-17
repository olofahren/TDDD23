using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFunctions : MonoBehaviour
{
    public void AssignStats(int unitNr, int lvl, int dmg, int mHP, int cHP, int def, int spe, int spec1, int spec2, int spec3, int maxEXP, float cEXP)
    {
        PlayerPrefs.SetInt("Chicken" + unitNr + "Lvl", lvl);
        PlayerPrefs.SetInt("Chicken" + unitNr + "dmg", dmg);
        PlayerPrefs.SetInt("Chicken" + unitNr + "maxHP", mHP);
        PlayerPrefs.SetInt("Chicken" + unitNr + "cHP", cHP);
        PlayerPrefs.SetInt("Chicken" + unitNr + "def", def);
        PlayerPrefs.SetInt("Chicken" + unitNr + "speed", spe);
        PlayerPrefs.SetInt("Chicken" + unitNr + "special1", spec1);
        PlayerPrefs.SetInt("Chicken" + unitNr + "special2", spec2);
        PlayerPrefs.SetInt("Chicken" + unitNr + "special3", spec3);
        PlayerPrefs.SetInt("Chicken" + unitNr + "maxEXP", maxEXP);
        PlayerPrefs.SetFloat("Chicken" + unitNr + "cEXP", cEXP);

        Debug.Log("-BattleFunction - says: Chicken" + unitNr + " is lvl " + lvl);

    }

}
