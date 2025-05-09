using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
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
    public float FaceDir  { get; private set; }
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
            canJump = 2;
        }
    }
    private void FixedUpdate() //固定時間運行
    {
        
      Move();
        
    }
    public void Move()
    {
        PlayerRB.velocity = new Vector2(inputLook.x * Speed, PlayerRB.velocity.y);

         FaceDir = (int)transform.localScale.x;

        if (inputLook.x > 0)
            FaceDir = 1;
        if (inputLook.x < 0)
            FaceDir = -1;

        //人物翻轉
        transform.localScale = new Vector3(FaceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)  //是否跳躍判定
    {
        //Debug.Log("jump");
        if (canJump > 0)
        {
           // Debug.Log(PlayerRB.velocity);
            PlayerRB.velocity = Vector2.up * JumpPower;  //執行
            isJump = true;
            if (isJump == true)
            {
                //Debug.Log("jump up");
                canJump--;
            }
        }

    }
            
    public int maxHealth = 3; // 玩家最大生命值
    private int currentHealth; // 玩家當前生命值

    [SerializeField]private SpriteRenderer spriteRenderer; // 用於改變玩家顏色
    private bool canHurt =true;
    private void Start()
    {
        currentHealth = maxHealth; // 初始化當前生命值
    }

    private void OnCollisionEnter2D(Collision2D feet)
    {
        if (feet.gameObject.CompareTag("Enemy") && canHurt)
        {
            TakeDamage();
            StartCoroutine(CanHurtTimer());
        }

        if (feet.gameObject.CompareTag("Ground") || feet.gameObject.CompareTag("SkillUse"))
        {
            isGround = true;

            //Debug.Log("isGround");
        }

    }

    

    private void OnCollisionExit2D(Collision2D feet)
    {
        if (feet.gameObject.CompareTag("Ground") || feet.gameObject.CompareTag("SkillUse"))
        {
            isGround = false;
        }

    }


    private IEnumerator CanHurtTimer()
    {
        canHurt = false;
        yield return new WaitForSeconds(1f);
        canHurt = true;
    }

    private void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            FlashRed();
        }
    }

    public void Die()
    {
        // 玩家死亡處理邏輯
        Debug.Log("Player Died");

        // 停止遊戲：將時間設置為 0（暫停遊戲）
        Time.timeScale = 0;

        // 停止玩家的操作：禁用物理和碰撞，保持玩家物件存在但不可見
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Collider2D[] colliders = gameObject.GetComponentsInChildren<Collider2D>();
        Rigidbody2D[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody2D>();

        // 禁用渲染、碰撞和物理
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        // 顯示 Game Over 畫面
        GameObject.FindObjectOfType<GameOverManager>().ShowGameOver();
    }






    private void FlashRed()
    {
        spriteRenderer.color = Color.white;
        spriteRenderer.DOColor(Color.red, 1f).SetLoops(4, LoopType.Yoyo);
        Debug.Log("Player");

    }
}


