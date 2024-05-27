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
        Debug.Log("jump");
        if (canJump > 0)
        {
            Debug.Log(PlayerRB.velocity);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&&canHurt)
        {
            TakeDamage();
            StartCoroutine(CanHurtTimer());
        }

    if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("SkillUse"))
    {
        isGround = true;
    }

}

    private void OnCollisionExit2D(Collision2D feet)
    {
        if (feet.gameObject.CompareTag("Ground") || feet.gameObject.CompareTag("SkillUse"))
        {
            isGround = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone") )
        {
            Die();
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

    private void Die()
    {
        // 玩家死亡處理邏輯，比如重新開始遊戲或顯示死亡畫面
        Debug.Log("Player Died");
        // 停止所有玩家的操作，這裡可以根據需求進行調整
        Destroy(gameObject);
    }

    private void FlashRed()
    {
        spriteRenderer.color = Color.white;
        spriteRenderer.DOColor(Color.red, 1f).SetLoops(6, LoopType.Yoyo);
        Debug.Log("Player");

    }
}


