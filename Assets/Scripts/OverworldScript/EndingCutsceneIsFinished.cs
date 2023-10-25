using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndingCutsceneIsFinished : MonoBehaviour
{
    public PlayableDirector director;
    public TMP_Text eggCounterText;
    private List<int> collectedEggs;
    private int eggCounterValue;
    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();

    }

    // Update is called once per frame
    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            PlayerPrefs.SetString("currentWorld", "Ending Screen");
            SceneManager.LoadScene("Ending Screen");
        }
            

    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }


    public void loadNextScene()
    {
        PlayerPrefs.SetString("currentWorld", "Ending Screen");
        SceneManager.LoadScene("Ending Screen");
    }
   
}
