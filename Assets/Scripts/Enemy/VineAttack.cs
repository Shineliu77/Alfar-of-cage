using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAttack : MonoBehaviour
{
    public int vineDamage = 10; // 藤蔓傷害
    public float attackDuration = 2f; // 藤蔓存在時間

    void Start()
    {
        Destroy(gameObject, attackDuration); // 一段時間後自動銷毀
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(vineDamage);
            }
        }
    }
}
