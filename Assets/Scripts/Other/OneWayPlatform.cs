using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D platformCollider;

    void Start()
    {
        // ���o�a�O�� Collider
        platformCollider = GetComponent<Collider2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("player")) // �T�{�I�����O���a
        {
            // ���o���a�� Rigidbody �t��
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.y > 0) // �u���b���W���ɭ�
            {
                Physics2D.IgnoreCollision(other, platformCollider, true); // �����I��
            }
            else
            {
                Physics2D.IgnoreCollision(other, platformCollider, false); // �}�ҸI��
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            // �T�O���}�ɫ�_�I��
            Physics2D.IgnoreCollision(other, platformCollider, false);
        }
    }
}
