using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float returnSpeed = 3f;
    public float attackCooldown = 2f;
    public int damage = 10;
    public int enemyHealth = 50;
    public int maxHealth = 50; // 加入最大血量
    public float knockbackForce = 5f;

    public float detectionRangeX = 5f; // X 軸偵測範圍
    public Transform player;
    public Transform startingPosition;

    private float lastAttackTime = 0f;
    private bool isPaused = false;
    private float pauseTimer = 0f;
    private bool isReturning = false;
    private Collider2D enemyCollider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Color pausedColor = new Color(0.55f, 0.64f, 0.63f); // 8CA2A0

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

        enemyCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

            if (pauseTimer <= 0.5f)
            {
                float flashSpeed = 10f;
                spriteRenderer.color = Mathf.FloorToInt(Time.time * flashSpeed) % 2 == 0 ? Color.white : pausedColor;
            }

            if (pauseTimer <= 0)
            {
                isPaused = false;
                if (enemyCollider != null)
                    enemyCollider.enabled = true;
                if (animator != null)
                    animator.enabled = true;
                spriteRenderer.color = Color.white;
            }
            return;
        }

        float distanceToPlayerX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceToStart = Vector2.Distance(transform.position, startingPosition.position);

        if (distanceToPlayerX > detectionRangeX)
        {
            isReturning = true;
            ReturnToStart(distanceToStart);
        }
        else
        {
            isReturning = false;
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * chaseSpeed * Time.deltaTime);
        FlipEnemy(direction.x);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPaused)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
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
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startingPosition.position;
        enemyHealth = maxHealth;
    }

    public void Pause(float duration)
    {
        isPaused = true;
        pauseTimer = duration;
        if (enemyCollider != null)
            enemyCollider.enabled = false;
        if (animator != null)
            animator.enabled = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = pausedColor;
        }
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
