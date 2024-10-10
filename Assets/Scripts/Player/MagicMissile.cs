using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    private float damage;
    private float range;
    private Vector3 startPosition;

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    public void SetRange(float missileRange)
    {
        range = missileRange;
        startPosition = transform.position;
    }

    void Update()
    {
        // 檢查飛彈是否飛出了範圍
        if (Vector3.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // 對敵人造成傷害
            }
            Destroy(gameObject);  // 碰撞後銷毀飛彈
        }
    }
}
