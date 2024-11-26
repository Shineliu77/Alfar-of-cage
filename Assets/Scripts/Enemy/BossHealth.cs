using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;  // �̤j��q
    private int currentHealth;    // ��e��q

    void Start()
    {
        // ��l�Ʀ�q���̤j��q
        currentHealth = maxHealth;
    }

    // �� BOSS ����ˮ`�����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // �� BOSS ���`�ɪ��޿�
    void Die()
    {
        Debug.Log("BOSS is dead!");
        // �b�o�̥i�H�[�J���`�ʵe�B���ĩξP�������޿�
        Destroy(gameObject); // �P�� BOSS ����
    }

    // ���o��e��q
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // ���o�̤j��q
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}

