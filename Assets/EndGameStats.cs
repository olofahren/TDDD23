using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EndGameStats : MonoBehaviour
{
    public TextMeshProUGUI noOfEggsStat;
    public TextMeshProUGUI noOfFightsStat;

    private List<int> collectedEggs;
    private List<int> completedBattles;

    // Start is called before the first frame update
    void Start()
    {
        collectedEggs = PlayerPrefsExtra.GetList<int>("collectedEggs2");
        completedBattles = PlayerPrefsExtra.GetList<int>("completedBattles");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (collectedEggs.Count != 20)
        {
            noOfEggsStat.text = "You collected " + (collectedEggs.Sum() - 1) + " of 20 eggs. Better luck collecting them all next time!";
        }
        else
        {
            noOfEggsStat.text = "You collected " + (collectedEggs.Sum() - 1) + " of 20 eggs. ¨Good job collecting all eggs!";

        }

        if(completedBattles.Count != 26)
        {
            noOfFightsStat.text = "You fought " + completedBattles.Sum() + " battles! You have more fight left in you!";

        }
        else
        {
            noOfFightsStat.text = "You fought " + completedBattles.Sum() + " battles! You defeated all enemies!";

        }
    }
}
