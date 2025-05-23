using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimetion : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D PL;
    public bool InUseSkill;
    public bool IsJump;
    [SerializeField]private Animator Skill;


    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
       
    }
    private void Update()
    {
        SetAnimetion();


        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("Use");
            InUseSkill = true;
            Skill.ResetTrigger("play");
            Skill.SetTrigger("play");
        }
        else
        {
            InUseSkill = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Space");
            IsJump = true;
        }
        else
        {
            IsJump = false;
        }
    }

    private void SetAnimetion()
    {
        anim.SetFloat("velocityx", Mathf.Abs(PL.velocity.x));
        anim.SetBool("skillkey", InUseSkill);
        anim.SetBool("JumpYN", IsJump);
    }



}

