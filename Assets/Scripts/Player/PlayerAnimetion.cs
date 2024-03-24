using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimetion : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D PL;
    public bool InUseSkill;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimetion();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("Q");
            InUseSkill = true;
        }
        else
        {
            InUseSkill = false;
        }
    }

    private void SetAnimetion()
    {
        anim.SetFloat("velocityx", Mathf.Abs(PL.velocity.x));
        anim.SetBool("skillkey", InUseSkill);

    }


    
}

