using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimetion : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D RB;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        SetAnimetion();
    }

    private void SetAnimetion()
    {
        anim.SetFloat("velocityx", Mathf.Abs(RB.velocity.x));
    }

}
