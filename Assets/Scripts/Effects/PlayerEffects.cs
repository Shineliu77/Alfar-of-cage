using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEffects : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D PL;
    public bool Qhit;

    private void Update()
    {
        SetAnimetion();


        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q");
            Qhit = true;
        }
        else
        {
            Qhit = false;

        }

    }

    private void SetAnimetion()
    {
        anim.SetBool("UseSkill", Qhit);
    }

}