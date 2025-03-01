using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float detectionRange = 10f;
    public float attackCooldown = 2f;
    public int baseDamage = 10;
    public int maxDamage = 30;
    public float knockbackForce = 5f;
    public float waitTimeAtPoint = 5f; // 停留時間
    public float spikeYPosition = -3f; // 地刺固定的 Y 軸位置

    public Transform player;
    public Transform[] movePoints;
    public GameObject spikePrefab;
    public Animator bossAnimator;

    private int currentMovePointIndex = 0;
    private float lastAttackTime = 0f;
    private bool isWaiting = false;
    private Rigidbody2D rb;
    private BossHealth bossHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (!isWaiting)
        {
            MoveBoss();
            AttackPlayer();
        }
    }

    void MoveBoss()
    {
        if (movePoints.Length > 0)
        {
            float distanceToTarget = Vector2.Distance(transform.position, movePoints[currentMovePointIndex].position);
            transform.position = Vector2.MoveTowards(transform.position, movePoints[currentMovePointIndex].position, chaseSpeed * Time.deltaTime);

            if (distanceToTarget < 0.1f)
            {
                StartCoroutine(WaitAndAttack());
            }
        }
    }

    IEnumerator WaitAndAttack()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTimeAtPoint);
        bossAnimator.SetTrigger("SpikeAttack"); // 播放攻擊動畫
    }

    public void SpawnSpikes()
    {
        Vector2 spikeLeftPos = new Vector2(transform.position.x - 2, spikeYPosition);
        Vector2 spikeRightPos = new Vector2(transform.position.x + 2, spikeYPosition);

        Instantiate(spikePrefab, spikeLeftPos, Quaternion.identity);
        Instantiate(spikePrefab, spikeRightPos, Quaternion.identity);

        currentMovePointIndex = (currentMovePointIndex + 1) % movePoints.Length;
        isWaiting = false;
    }

    void AttackPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange && Time.time - lastAttackTime >= attackCooldown)
        {
            float impactSpeed = rb.velocity.magnitude;
            int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                KnockbackPlayer();
            }
            lastAttackTime = Time.time;
        }
    }

    void KnockbackPlayer()
    {
        Vector2 knockbackDirection = (player.position - transform.position).normalized;
        player.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float impactSpeed = rb.velocity.magnitude;
            int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                KnockbackPlayer();
            }
            lastAttackTime = Time.time;
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
