using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{

    public float chaseSpeed = 5f;
    public float detectionRange = 10f;
    public float attackCooldown = 2f;
    public int baseDamage = 10;
    public int maxDamage = 30;
    public int enemyHealth = 50;
    public float knockbackForce = 5f; // 撞擊時給玩家的擊退力
    public Transform player;

    private float lastAttackTime = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // 追擊玩家
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * chaseSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                // 計算傷害
                float impactSpeed = rb.velocity.magnitude;
                int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);

                // 對玩家造成傷害並施加擊退
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    playerHealth.TakeDamage(damage, knockbackDirection * knockbackForce);
                }

                // 記錄上次攻擊時間
                lastAttackTime = Time.time;

                // 敵人後退簡單移動
                StartCoroutine(RetreatAfterHit());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            TakeDamage(10); // 例如飛彈造成10點傷害
            Destroy(collision.gameObject);

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator RetreatAfterHit()
    {
        Vector2 retreatDirection = -rb.velocity.normalized;
        rb.velocity = retreatDirection * 2f; // 簡單的後退效果

        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector2.zero;
    }


}
