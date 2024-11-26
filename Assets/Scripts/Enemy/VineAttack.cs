using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAttack : MonoBehaviour
{
    public int vineDamage = 10; // �ý��ˮ`
    public float attackDuration = 2f; // �ý��s�b�ɶ�

    void Start()
    {
        Destroy(gameObject, attackDuration); // �@�q�ɶ���۰ʾP��
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
