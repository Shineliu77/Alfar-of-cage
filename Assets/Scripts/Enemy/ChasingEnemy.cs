using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{

    public float detectionRange = 10f;        // 偵測玩家的範圍
    public float normalSpeed = 3f;           // 正常移動速度
    public float dashSpeed = 10f;            // 衝刺速度
    public float attackCooldown = 2f;        // 每次攻擊後的冷卻時間
    public float retreatDistance = 3f;       // 攻擊後退後的距離
    public float baseDamage = 5f;            // 基礎傷害
    public float maxDamage = 20f;            // 最大傷害
    public float movementRange = 10f;        // 敵人可活動的最大範圍

    private Transform player;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canAttack = true;
    private Vector2 initialPosition;         // 初始位置，用於範圍限制
    private Vector2 lastPlayerPosition;      // 最後一次衝刺時玩家的位置

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // 記錄敵人的初始位置
    }

    void Update()
    {
        if (player == null)
            return;

        // 檢查玩家是否在偵測範圍內
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && canAttack)
        {
            // 玩家進入範圍且可以攻擊，進行衝刺
            StartDash();
        }
        else if (!isDashing)
        {
            // 如果玩家不在範圍內，回到初始位置
            PatrolOrReturn();
        }
    }

    void StartDash()
    {
        isDashing = true;
        canAttack = false;
        lastPlayerPosition = player.position;  // 轉換 player.position 為 Vector2

        // 計算衝刺方向並設置速度
        Vector2 dashDirection = (lastPlayerPosition - (Vector2)transform.position).normalized;

        // 確保敵人不會衝出活動範圍
        Vector2 predictedPosition = (Vector2)transform.position + dashDirection * dashSpeed * Time.deltaTime;
        if (Vector2.Distance(initialPosition, predictedPosition) <= movementRange)
        {
            rb.velocity = dashDirection * dashSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero; // 如果會超出範圍，取消衝刺
            isDashing = false;
        }
    }

    void PatrolOrReturn()
    {
        // 如果超出活動範圍，返回初始位置
        if (Vector2.Distance(initialPosition, transform.position) > movementRange)
        {
            Vector2 returnDirection = (initialPosition - (Vector2)transform.position).normalized;
            rb.velocity = returnDirection * normalSpeed;
        }
        else
        {
            // 在範圍內巡邏（可以加上巡邏邏輯）
            rb.velocity = Vector2.zero; // 這裡可以添加巡邏行為
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Player"))
        {
            // 停止衝刺
            isDashing = false;
            rb.velocity = Vector2.zero;

            // 計算衝刺傷害，根據衝刺速度增加傷害
            float impactSpeed = dashSpeed;  // 假設衝刺時的最大速度
            float damage = baseDamage + (impactSpeed / dashSpeed) * maxDamage;

            // 對玩家造成傷害
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // 開始退後並進行攻擊冷卻
            StartCoroutine(RetreatAndCooldown());
        }
    }

    System.Collections.IEnumerator RetreatAndCooldown()
    {
        // 退後
        Vector2 retreatDirection = ((Vector2)transform.position - lastPlayerPosition).normalized;
        rb.velocity = retreatDirection * normalSpeed;

        // 等待一段時間以模擬退後
        yield return new WaitForSeconds(0.5f);

        // 停止退後
        rb.velocity = Vector2.zero;

        // 等待攻擊冷卻
        yield return new WaitForSeconds(attackCooldown);

        // 恢復攻擊能力
        canAttack = true;
    }
}
