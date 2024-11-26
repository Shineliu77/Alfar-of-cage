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
    public float knockbackForce = 5f; // �����ɵ����a�����h�O
    public Transform player;
    public Transform[] movePoints;   // �w�q�����I

    private float lastAttackTime = 0f;
    private Rigidbody2D rb;
    private BossHealth bossHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>(); // �T�O���o BossHealth

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
            // ���] BOSS �|�b�����I�����`������
            // �o�̥i�H�[�J�޿�A�� BOSS ���C�Ӳ����I����
        }
    }

    void AttackPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange && Time.time - lastAttackTime >= attackCooldown)
        {
            // �p��ˮ`
            float impactSpeed = rb.velocity.magnitude;
            int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);

            // �缾�a�y���ˮ`�ìI�[���h
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
        }
    }

    // ���� BOSS ����ˮ`
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                // �p��ˮ`
                float impactSpeed = rb.velocity.magnitude;
                int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);

                // �缾�a�y���ˮ`
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
            bossHealth.TakeDamage(damage); // �b BossHealth �}�����B�z�ˮ`
        }
    }
}


