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

    [Header("­p®É¾¹")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

   
    private void Start()
    {
        canUse = true;
        waitTimeCounter = waitTime;
    }

    void Update()
    {
        flower.SetActive(canUse);
        smallflower.SetActive(wait);
        //timeCounter();

        if(playerin==true && Input.GetKeyDown(KeyCode.Q))
        {
            wait = true;
            canUse = false;
        }

    }

    /*public void timeCounter()
    {
        if(wait==true)
        {
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter<=0)
            {
                wait = false;
                canUse = true;
                waitTimeCounter = waitTime;
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gameplayer"))
        {
            playerin = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gameplayer"))
        {
            playerin = false;
        }
    }

}
