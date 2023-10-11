using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class cutsceneIsFinished : MonoBehaviour
{
    public PlayableDirector director;
    public TMP_Text eggCounterText;
    private List<int> collectedEggs;
    private int eggCounterValue;
    GameObject egg1;
    GameObject egg2;
    GameObject egg3;
    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
        egg1 = GameObject.Find("1");
        egg2 = GameObject.Find("2");
        egg3 = GameObject.Find("3");
    }

    // Update is called once per frame
    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
            PlayerPrefs.SetString("currentWorld", "Main World");
            SceneManager.LoadScene("Main World");

    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

    public void RemoveEggs()
    {
        collectedEggs = PlayerPrefsExtra.GetList<int>("collectedEggs");
        collectedEggs.Clear();
        collectedEggs = Enumerable.Repeat(0, 11).ToList();
        PlayerPrefsExtra.SetList("collectedEggs", collectedEggs);
        eggCounterValue = collectedEggs.Sum();
        eggCounterText.text = eggCounterValue.ToString() + "/20";

        egg1.GetComponent<Renderer>().enabled = true;
        egg2.GetComponent<Renderer>().enabled = true;
        egg3.GetComponent<Renderer>().enabled = true;

        egg1.GetComponent<pickUpEgg>().playerHasPickedUpEgg = false;
        egg2.GetComponent<pickUpEgg>().playerHasPickedUpEgg = false;
        egg3.GetComponent<pickUpEgg>().playerHasPickedUpEgg = false;




    }
}
