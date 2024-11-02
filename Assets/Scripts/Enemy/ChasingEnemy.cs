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
    public float knockbackForce = 5f; // �����ɵ����a�����h�O
    public Transform player;

    private float lastAttackTime = 0f;
    private Rigidbody2D rb;
    private bool isPaused = false;
    private float pauseTimer = 0f;

    

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


        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isPaused = false;
            }
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // �l�����a
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
                // �p��ˮ`
                float impactSpeed = rb.velocity.magnitude;
                int damage = Mathf.Clamp((int)(baseDamage * impactSpeed), baseDamage, maxDamage);

                // �缾�a�y���ˮ`�ìI�[���h
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                   
                    playerHealth.TakeDamage(damage);
                }

                // �O���W�������ɶ�
                lastAttackTime = Time.time;

             
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeDamage(10); // �Ҧp���u�y��10�I�ˮ`
            Destroy(collision.gameObject);

         
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
    }
 


}
