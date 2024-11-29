using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject DialogueObject;    // 觸發對話的物件

    void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //開啟對話物件
            DialogueObject.SetActive(true);
            //關閉碰觸的物件
            this.gameObject.SetActive(false);
        }
    }
   
}
