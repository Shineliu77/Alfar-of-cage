using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isPaused = false;
    private float pauseTimer = 0f;

    public float moveSpeed = 2f; // 敵人移動速度
    public Transform pointA; // 巡邏點 A
    public Transform pointB; // 巡邏點 B
    private Vector3 targetPosition; // 當前目標位置

    void Start()
    {
        targetPosition = pointA.position; // 初始化目標位置為點 A
    }

    void Update()
    {
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isPaused = false;
            }
            return;
        }

        // 實現敵人的移動或攻擊行為
        Move();
    }

    private void Move()
    {
        // 移動敵人到目標位置
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 檢查是否到達目標位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 反轉目標位置
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }

        int faceDir = (int)transform.localScale.x;

        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    

    public void Pause(float duration)
    {
        isPaused = true;
        pauseTimer = duration;
    }
}
