using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{

    public void continueGame()
    {
        
        SceneManager.LoadScene(PlayerPrefs.GetString("currentWorld"));
    }

}
