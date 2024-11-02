using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyLeaf : MonoBehaviour
{
   [SerializeField] float bounceForce = 10f; // 彈跳的力度
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("fk");
        if (collision.gameObject.CompareTag("Player"))
        {

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Debug.Log("UP");
                // 重置玩家的垂直速度，避免疊加效果
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                // 施加向上的彈跳力
                playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
