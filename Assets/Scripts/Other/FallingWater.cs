using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWater : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �ˬd�O�_�I����е��� Ground ������
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ����w�I��a���ɾP�����w
            Destroy(gameObject);
        }
    }
}
