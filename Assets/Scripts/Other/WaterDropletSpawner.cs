using UnityEngine;

public class WaterDropletSpawner : MonoBehaviour
{
    public GameObject fallingWaterPrefab;  // �w�������w�s����
    public Transform spawnPoint;           // ���w�ͦ���m
    public float spawnInterval = 3f;       // �ͦ����w�����j�]��^

    private float nextSpawnTime = 0f;      // �U�@���ͦ����w���ɶ�

    void Update()
    {
        // �p�G�w�g�L�F���w�ɶ��A�ͦ����w
        if (Time.time >= nextSpawnTime)
        {
            SpawnWaterDroplet();
            nextSpawnTime = Time.time + spawnInterval;  // ��s�U�@���ͦ��ɶ�
        }
    }

    void SpawnWaterDroplet()
    {
        // �ͦ����w�ó]�m����
        GameObject waterDroplet = Instantiate(fallingWaterPrefab, spawnPoint.position, Quaternion.identity);
        waterDroplet.tag = "FallingWater";  // �]�m����
        waterDroplet.layer = LayerMask.NameToLayer("WaterLayer");  // �]�m�h�� WaterLayer
    }
}
