using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLeaf : MonoBehaviour
{
   [SerializeField] float bounceForce = 10f; // �u�����O��
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("fk");
        if (collision.gameObject.CompareTag("Player"))
        {

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Debug.Log("UP");
                // ���m���a�������t�סA�קK�|�[�ĪG
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                // �I�[�V�W���u���O
                playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
