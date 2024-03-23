using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsAnimetion : MonoBehaviour
{
    private Animator anim;
    public bool InUseSkill;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    

    private void Update()
    {
        SetAnimation();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("Q");
            InUseSkill = true;
        }
        else
        {
            InUseSkill = false;
        }
    }

    public void SetAnimation()
    {
        anim.SetBool("skillkey",InUseSkill);
    }

    

    

}




