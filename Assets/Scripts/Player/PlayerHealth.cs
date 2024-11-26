using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("玩家血量設定")]
    public int playerHealth = 100; // 當前血量
    public int maxHealth = 100; // 最大血量

    [Header("血條 UI")]
    public Slider healthSlider; // 血條 Slider

    [Header("擊退設定")]
    public float knockbackDistance = 2f; // 擊退距離
    public float knockbackTime = 0.2f; // 擊退時間
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float timer;
    private bool isKnockback;

    [Header("受傷音效")]
    public AudioSource damageAudio;

    private Rigidbody2D rb;
    [SerializeField] private PlayerControl control;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 初始化血條
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = playerHealth;
        }
    }

    void Update()
    {
        // 擊退邏輯
        if (isKnockback)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer / knockbackTime);

            if (timer >= knockbackTime)
            {
                isKnockback = false;
                timer = 0;
            }
        }
    }

    // 玩家受到傷害
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);

        // 撥放受傷音效
        if (damageAudio != null)
        {
            damageAudio.Play();
        }

        // 更新血條
        if (healthSlider != null)
        {
            healthSlider.value = playerHealth;
        }

        // 判斷玩家是否死亡
        if (playerHealth <= 0)
        {
            Debug.Log("Player Died");
            GameObject.FindObjectOfType<GameOverManager>()?.ShowGameOver();
        }

        // 施加擊退效果
        ApplyKnockback();
    }

    // 擊退邏輯
    public void ApplyKnockback()
    {
        isKnockback = true;

        Vector3 direction = control.FaceDir == 1 ? Vector3.left : Vector3.right;
        startPosition = transform.position;
        endPosition = startPosition + direction * knockbackDistance;
    }
}
