using UnityEngine;

public class WaterDropletSpawner : MonoBehaviour
{
    public GameObject fallingWaterPrefab;  // 滴落水的預製物件
    public Transform spawnPoint;           // 水滴生成位置
    public float spawnInterval = 3f;       // 生成水滴的間隔（秒）

    private float nextSpawnTime = 0f;      // 下一次生成水滴的時間

    void Update()
    {
        // 如果已經過了指定時間，生成水滴
        if (Time.time >= nextSpawnTime)
        {
            SpawnWaterDroplet();
            nextSpawnTime = Time.time + spawnInterval;  // 更新下一次生成時間
        }
    }

    void SpawnWaterDroplet()
    {
        // 生成水滴並設置標籤
        GameObject waterDroplet = Instantiate(fallingWaterPrefab, spawnPoint.position, Quaternion.identity);
        waterDroplet.tag = "FallingWater";  // 設置標籤
        waterDroplet.layer = LayerMask.NameToLayer("WaterLayer");  // 設置層為 WaterLayer
    }
}
