using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeatherSkill : MonoBehaviour
{
    public GameObject icePrefab;     // 結冰後的物件預製物件
    public float freezeRange = 5f;   // 技能範圍（單位：Unity單位）

    void Update()
    {
        // 按下 Q 鍵使用技能
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill();
        }
    }

    void UseSkill()
    {
        Debug.Log("角色技能啟動：結冰範圍內的水！");
        FreezeAllWaterInRange();
    }

    void FreezeAllWaterInRange()
    {
        // 找到場景中的所有 GroundWater
        GameObject[] groundWaters = GameObject.FindGameObjectsWithTag("GroundWater");
        foreach (GameObject water in groundWaters)
        {
            if (IsWithinRange(water.transform.position))
            {
                FreezeGroundWater(water);
            }
        }

        // 找到場景中的所有 FallingWater
        GameObject[] fallingWaters = GameObject.FindGameObjectsWithTag("FallingWater");
        foreach (GameObject water in fallingWaters)
        {
            if (IsWithinRange(water.transform.position))
            {
                FreezeFallingWater(water);
            }
        }
    }

    bool IsWithinRange(Vector3 position)
    {
        // 檢查目標位置是否在範圍內
        return Vector3.Distance(transform.position, position) <= freezeRange;
    }

    void FreezeGroundWater(GameObject water)
    {
        // 替換為冰塊
        Instantiate(icePrefab, water.transform.position, Quaternion.identity);
        Destroy(water); // 刪除水
    }

    void FreezeFallingWater(GameObject water)
    {
        // 替換為冰塊
        Instantiate(icePrefab, water.transform.position, Quaternion.identity);
        Destroy(water); // 刪除水
    }
}
