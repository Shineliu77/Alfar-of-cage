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
    public string targetTag = "TargetObject"; // 指定物件的標籤
    public string treeTag = "tree"; // 樹的標籤
    public string iceTreeTag = "Ground"; // 冰樹的標籤

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // 按 Q 施放技能
        {
            FreezeWaterInRange();
            DisableTargetsInRange();
            ConvertTreesInRange();
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

    void DisableTargetsInRange()
    {
        // 檢測範圍內所有碰撞體
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // 如果碰撞到的是指定的物件，則關閉該物件
            if (hitCollider.CompareTag(targetTag))
            {
                hitCollider.gameObject.SetActive(false);
            }
        }
    }

    void ConvertTreesInRange()
    {
        // 檢測範圍內所有碰撞體
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // 如果碰撞到的是樹（tree），則關閉它並嘗試啟用對應的冰樹（icetree）
            if (hitCollider.CompareTag(treeTag))
            {
                // 只在樹物件仍然啟用時禁用它
                if (hitCollider.gameObject.activeSelf)
                {
                    Debug.Log($"Disabling tree: {hitCollider.name}");
                    hitCollider.gameObject.SetActive(false);

                    // 查找與 tree 同位置的 icetree
                    Transform parentTransform = hitCollider.transform.parent;
                    if (parentTransform != null)
                    {
                        foreach (Transform child in parentTransform)
                        {
                            if (child.CompareTag(iceTreeTag))
                            {
                                // 確保冰樹物件被啟用
                                if (!child.gameObject.activeSelf)
                                {
                                    Debug.Log($"Enabling ice tree: {child.name}");
                                    child.gameObject.SetActive(true);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }


    void FreezeGroundWater(Collider2D groundWater)
    {
        // 隱藏地面水，並顯示冰塊
        groundWater.gameObject.SetActive(false);
        GameObject ice = Instantiate(icePrefab, groundWater.transform.position, Quaternion.identity);
        ice.transform.localScale = groundWater.transform.localScale; // 設定冰塊大小與水相同
        ice.transform.position = new Vector3(groundWater.transform.position.x, groundWater.transform.position.y, 0);
    }

    void FreezeFallingWater(Collider2D fallingWater)
    {
        fallingWater.gameObject.SetActive(false);
        GameObject hail = Instantiate(hailPrefab, fallingWater.transform.position, Quaternion.identity);

        // 固定冰雹的大小為 (0.1, 0.1, 1)
        hail.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        hail.transform.position = new Vector3(fallingWater.transform.position.x, fallingWater.transform.position.y, 0);

        // 為冰雹添加對敵人傷害的邏輯
        HailDamage hailDamageScript = hail.AddComponent<HailDamage>();
        hailDamageScript.SetDamage(hailDamage);
    }
}
