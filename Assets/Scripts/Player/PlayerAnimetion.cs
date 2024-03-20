using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimetion : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D PL;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimetion();
    }

    private void SetAnimetion()
    {
        anim.SetFloat("velocityx", Mathf.Abs(PL.velocity.x));

    }


    
}

