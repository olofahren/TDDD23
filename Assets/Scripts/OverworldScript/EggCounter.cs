using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EggCounter : MonoBehaviour
{
    public TMP_Text eggCounterText;
    private int eggCounterValue;
    private int lastEggCounterValue;
    private List<int> collectedEggs;
    private Animator animator;

    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        collectedEggs = PlayerPrefsExtra.GetList<int>("collectedEggs");
        eggCounterValue = collectedEggs.Sum();
        eggCounterText.text = eggCounterValue.ToString() + "/10";

    }

    private void FixedUpdate()
    {
        collectedEggs = PlayerPrefsExtra.GetList<int>("collectedEggs");
        eggCounterValue = collectedEggs.Sum();


        if (eggCounterValue != lastEggCounterValue)
        {
            //collectedEggs.ForEach( egg => Debug.Log(egg));

            eggCounterText.text = eggCounterValue.ToString() + "/10";
            lastEggCounterValue = eggCounterValue;
            animator.SetTrigger("animateEggIcon");



        }

    }

}
