using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float detectionRange = 10f;
    public float attackCooldown = 2f;
    public int baseDamage = 10;
    public int maxDamage = 30;
    public float knockbackForce = 5f; // 撞擊時給玩家的擊退力
    public Transform player;
    public Transform[] movePoints;   // 定義移動點
    private int currentMovePointIndex = 0;  // 追蹤當前目標點的索引


    private float lastAttackTime = 0f;
    private Rigidbody2D rb;
    private BossHealth bossHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>(); // 確保取得 BossHealth

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        MoveBoss();
        AttackPlayer();
    }

    void MoveBoss()
    {
        if (movePoints.Length > 0)
        {
            float distanceToTarget = Vector2.Distance(transform.position, movePoints[currentMovePointIndex].position);

            // 移動到目標點
            transform.position = Vector2.MoveTowards(transform.position, movePoints[currentMovePointIndex].position, chaseSpeed * Time.deltaTime);

            // 當 BOSS 到達目標點後，切換到下一個移動點
            if (distanceToTarget < 0.1f) // 判斷是否到達目標點
            {
                // 切換到下一個點
                currentMovePointIndex = (currentMovePointIndex + 1) % movePoints.Length;  // 循環移動點
            }
        }
    }

        void AttackPlayer()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && Time.time - lastAttackTime >= attackCooldown)
            {
                // 計算傷害
                float impactSpeed = rb.velocity.magnitude;
                int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);
                // 對玩家造成傷害並施加擊退
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                lastAttackTime = Time.time;
            }
        }

        // 撞擊 BOSS 受到傷害
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    // 計算傷害
                    float impactSpeed = rb.velocity.magnitude;
                    int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);

                    // 對玩家造成傷害
                    PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                    }

                    lastAttackTime = Time.time;
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }
        }
    }


  
