using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimation : MonoBehaviour
{
    private Animator anim;
    private Unit unit; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (unit.currentHP == 0)
        {
            anim.SetBool("isDead", true);
        }
        else
        {
            anim.SetBool("isDead", false);
        }
    }

    public void PlayAttackAnimation()
    {
        anim.Play("ChickenAttack_Clip");
    }

    public void PlayHealAnimation()
    {
        anim.Play("ChickenSleeping_Clip");
    }

    public void PlayDamageAnimation()
    {
        anim.Play("ChickenDie_Clip_2");
    }

    public void PlaySpecialAttackAnimation()
    {
        anim.Play("ChickenPeck_Clip");
    }

}
