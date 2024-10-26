using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // ���a����ˮ`
    public void TakeDamage(int damage, Vector2 knockback)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("Player Died");
            // ���a���`�޿�
        }

        // �缾�a�I�[���h�ĪG
        ApplyKnockback(knockback);
    }

    // ���h�ĪG
    private void ApplyKnockback(Vector2 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse); // �缾�a�I�[���h
    }
}
