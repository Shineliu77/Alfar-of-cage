using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flower : MonoBehaviour
{
    public GameObject flower;
    public GameObject smallflower;
    public bool canUse;
    public bool playerin;

    [Header("等待時間")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    [Header("指定玩家")]
    public GameObject specificPlayer; // 指定的玩家物件

    private void Start()
    {
        canUse = true;
        //waitTimeCounter = waitTime;
    }

    void Update()
    {
        flower.SetActive(canUse);
        smallflower.SetActive(wait);

        // 確保只有特定玩家在範圍內時執行邏輯
        if (playerin && Input.GetKeyDown(KeyCode.Q))
        {
            wait = true;
            canUse = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 確認進入範圍的物件是否是指定的玩家
        if (collision.gameObject == specificPlayer)
        {
            playerin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 確認離開範圍的物件是否是指定的玩家
        if (collision.gameObject == specificPlayer)
        {
            playerin = false;
        }
    }
}
