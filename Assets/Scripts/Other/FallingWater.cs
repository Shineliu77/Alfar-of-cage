using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWater : MonoBehaviour
{
    public float lifetime = 5f; // 設置水滴的存活時間（秒）

    void Start()
    {
        // 在指定時間後銷毀水滴
        Destroy(gameObject, lifetime);
    }

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
