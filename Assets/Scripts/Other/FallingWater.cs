using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWater : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 檢查是否碰撞到標註為 Ground 的物件
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 當水滴碰到地面時銷毀水滴
            Destroy(gameObject);
        }
    }
}
