using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected bool isPaused = false;
    protected float pauseTimer = 0f;

    public float normalspeed;//一般速度
    public float chasespeed;//追擊速度
    public float currentspeed;//當前速度
    public Collider2D enemy;//敵人的碰撞體
    public Transform pointA, pointB; //巡邏點AB
    protected Vector3 targetPosition; // 當前目標位置
    protected int faceDir;//角色方向
    

    protected void Awake()
    {
        currentspeed = normalspeed;
        targetPosition = pointA.position; // 初始化目標位置為點 A
    }

    protected void Update()
    {
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            enemy.enabled = false;
            if (pauseTimer <= 0)
            {
                isPaused = false;
                enemy.enabled = true;
            }
            return;
        }

        // 實現敵人的移動或攻擊行為
        Move();
    }


    protected void Move()
    {
        // 移動敵人到目標位置
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentspeed * Time.deltaTime);

        // 檢查是否到達目標位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 反轉目標位置
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }

        if (targetPosition == pointA.position)
            faceDir = 1;
        if (targetPosition == pointB.position)
            faceDir = -1;

        //人物翻轉
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    protected void OnTriggerEnter2D(Collider2D haveplayer)
    {

        if (currentspeed == normalspeed && haveplayer.gameObject.CompareTag("Gameplayer"))
        {
            currentspeed = chasespeed;
        }
        return;

    }

    protected void OnTriggerExit2D(Collider2D haveplayer)
    {

        if (currentspeed == chasespeed && haveplayer.gameObject.CompareTag("Gameplayer"))
        {
            currentspeed = normalspeed;
        }
        return;

    }

    public void Pause(float duration)
    {
        isPaused = true;
        pauseTimer = duration;
    }
}
