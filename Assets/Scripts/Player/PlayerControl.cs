using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//�ҥΦۭq����J��Ҩt��

public class PlayerControl : MonoBehaviour
{
    public InputControl input;
    [Header("監測")]
    public Vector2 inputLook;
    [Header ("數值")]
    public float Speed;
    public float JumpPower;
    public Rigidbody2D PlayerRB;
    public int canJump = 1;
    public BoxCollider2D feet;

    private bool isJump;
    private bool isGround;

    private void Awake()  //在遊戲開始時第一個啟動
    {
        input = new InputControl();

        input.GamePlay.Jump.started += Jump;  //註冊名為Jump的函數方法

    }


    private void OnEnable()  //當物件開啟
    {
        input.Enable();
    }
    private void OnDisable()  //當物件關閉
    {
        input.Disable();
    }
    //控制系統隨物件開啟關閉改變


    // Update is called once per frame
    void Update()  //每幀更新
    {
        inputLook = input.GamePlay.Move.ReadValue<Vector2>();  

        if (isGround == true)
        {
            isJump = false;
            canJump = 1;
        }
    }
    private void FixedUpdate() //固定時間運行
    {
        
      Move();
        
    }
    public void Move()
    {
        PlayerRB.velocity = new Vector2(inputLook.x * Speed, PlayerRB.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputLook.x > 0)
            faceDir = 1;
        if (inputLook.x < 0)
            faceDir = -1;

        //人物翻轉
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)  //是否跳躍判定
    {
        // Debug.Log("jump");
        if (canJump > 0)
        {
            PlayerRB.velocity = Vector2.up * JumpPower;  //執行
            isJump = true;
            if (isJump == true)
            {
                //Debug.Log("jump up");
                canJump--;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D feet)
    {
        if (feet.CompareTag("Ground") || feet.CompareTag("SkillUse"))
        {
            isGround = true;
        }

    }
    private void OnTriggerExit2D(Collider2D feet)
    {
        if (feet.CompareTag("Ground") || feet.CompareTag("SkillUse"))
        {
            isGround = false;
        }
       
    }
}


