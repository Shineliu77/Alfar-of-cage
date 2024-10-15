using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_ : MonoBehaviour
{

    public GameObject magicMissilePrefab;   // 魔法飛彈預製物件
    public Transform firePoint;             // 發射點
    public float missileSpeed = 10f;        // 飛彈速度
    public float attackRange = 10f;         // 攻擊距離
    public float attackDamage = 5f;         // 攻擊傷害

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 右鍵按下時發射飛彈
        {
            FireMagicMissile();
        }
    }

    void FireMagicMissile()
    {

        // 計算玩家到滑鼠的方向
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // 創建飛彈並設置其初始位置與方向
        GameObject missile = Instantiate(magicMissilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * missileSpeed;

        // 設置飛彈速度和方向
        rb.velocity = direction * missileSpeed;

        // 設定飛彈的攻擊傷害與最大飛行距離
        MagicMissile missileScript = missile.GetComponent<MagicMissile>();
        missileScript.SetDamage(attackDamage);
        missileScript.SetRange(attackRange);
    }
}
