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
        // �ˬd���u�O�_���X�F�d��
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
                enemyHealth.TakeDamage(damage);  // ��ĤH�y���ˮ`
            }
            Destroy(gameObject);  // �I����P�����u
        }
    }
}
