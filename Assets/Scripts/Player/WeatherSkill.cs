using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSkill : MonoBehaviour
{
    public float skillRadius = 5f; // 技能範圍半徑
    public LayerMask waterLayer; // 水層
    public GameObject icePrefab; // 冰塊預製物件（用於地面水）
    public GameObject hailPrefab; // 冰雹預製物件（用於滴落水）
    public float hailDamage = 5f; // 冰雹造成的傷害

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // 按 Q 施放技能
        {
            FreezeWaterInRange();
        }
    }

    void FreezeWaterInRange()
    {
        // 取得角色位置，並檢查範圍內所有的水
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius, waterLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // 如果是地面上的水
            if (hitCollider.CompareTag("GroundWater"))
            {
                FreezeGroundWater(hitCollider);
            }
            // 如果是滴落的水
            else if (hitCollider.CompareTag("FallingWater"))
            {
                FreezeFallingWater(hitCollider);
            }
        }
    }

    void FreezeGroundWater(Collider2D groundWater)
    {
        // 隱藏地面水，並顯示冰塊
        groundWater.gameObject.SetActive(false);
        GameObject ice = Instantiate(icePrefab, groundWater.transform.position, Quaternion.identity);
        ice.transform.localScale = groundWater.transform.localScale; // 設定冰塊大小與水相同
    }

    void FreezeFallingWater(Collider2D fallingWater)
    {
        // 隱藏滴落水，並顯示冰雹
        fallingWater.gameObject.SetActive(false);
        GameObject hail = Instantiate(hailPrefab, fallingWater.transform.position, Quaternion.identity);
        hail.transform.localScale = fallingWater.transform.localScale; // 設定冰雹大小與滴水相同

        // 為冰雹添加對敵人傷害的邏輯
        HailDamage hailDamageScript = hail.AddComponent<HailDamage>();
        hailDamageScript.SetDamage(hailDamage);
    }

  
}
