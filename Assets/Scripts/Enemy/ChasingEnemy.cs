using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float detectionRange = 10f;
    public float returnSpeed = 3f;
    public Vector2 patrolArea = new Vector2(15f, 15f);
    public float attackCooldown = 2f;
    public int baseDamage = 10;
    public int maxDamage = 30;
    public int enemyHealth = 50;
    public float knockbackForce = 5f;
    public Transform player;
    public Transform startingPosition;

    private float lastAttackTime = 0f;
    private bool isPaused = false;
    private float pauseTimer = 0f;

    private bool isReturning = false;
    private Collider2D enemyCollider;
    private Animator animator; // 新增 Animator 引用

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (startingPosition == null)
        {
            GameObject start = new GameObject("StartingPosition");
            start.transform.position = transform.position;
            startingPosition = start.transform;
        }

        // 取得敵人的 Collider
        enemyCollider = GetComponent<Collider2D>();

        // 取得敵人的 Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No Animator found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isPaused = false;
                // 重新啟用敵人碰撞體
                if (enemyCollider != null)
                    enemyCollider.enabled = true;

                // 啟用動畫
                if (animator != null)
                    animator.enabled = true;
            }
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToStart = Vector2.Distance(transform.position, startingPosition.position);

        if (distanceToPlayer > detectionRange || !IsWithinPatrolArea(transform.position))
        {
            isReturning = true;
            ReturnToStart(distanceToStart);
        }
        else
        {
            isReturning = false;
            ChasePlayer(distanceToPlayer);
        }
    }

    void ChasePlayer(float distanceToPlayer)
    {
        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * chaseSpeed * Time.deltaTime);

            FlipEnemy(direction.x);
        }
    }

    void ReturnToStart(float distanceToStart)
    {
        if (distanceToStart > 0.1f)
        {
            Vector2 direction = (startingPosition.position - transform.position).normalized;
            transform.Translate(direction * returnSpeed * Time.deltaTime);

            FlipEnemy(direction.x);
        }
        else
        {
            transform.position = startingPosition.position;
            ForceFaceLeft();
        }
    }

    bool IsWithinPatrolArea(Vector2 position)
    {
        Vector2 startPosition2D = new Vector2(startingPosition.position.x, startingPosition.position.y);
        return Vector2.Distance(startPosition2D, position) <= patrolArea.x;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPaused)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                float impactSpeed = chaseSpeed;
                int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);

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
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Pause(float duration)
    {
        isPaused = true;
        pauseTimer = duration;

        // 當敵人被暫停時，禁用碰撞體，讓玩家可以穿過敵人
        if (enemyCollider != null)
            enemyCollider.enabled = false;

        // 暫停動畫
        if (animator != null)
            animator.enabled = false;
    }

    void FlipEnemy(float directionX)
    {
        if (directionX > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionX < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void ForceFaceLeft()
    {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
