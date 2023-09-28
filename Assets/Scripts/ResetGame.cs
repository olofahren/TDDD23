using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private List<int> collectedEggs = Enumerable.Repeat(0, 10).ToList();
    public void resetGame()
    {
        collectedEggs.ForEach(egg => Debug.Log(egg));
        PlayerPrefs.SetFloat("PlayerX", -2.98f);
        PlayerPrefs.SetFloat("PlayerY", -60.02f);
        PlayerPrefs.SetFloat("PlayerZ", -5.0f);
        PlayerPrefs.SetInt("BattleIsFinished", 0);
        PlayerPrefsExtra.SetList("collectedEggs", collectedEggs);

        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentWorld"));

    }
}
