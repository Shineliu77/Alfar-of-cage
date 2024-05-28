using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{

    public Transform posA, posB; //巡邏點AB
    public float speed; //速度
    Vector3 tranGetPots; 

    private bool isPaused = false;
    private float pauseTimer = 0f;

    private void Start()
    {
        tranGetPots = posB.position;
    }

    private void Update()
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

        if (Vector2.Distance(transform.position, posA.position) < 0.05f)
        {
            tranGetPots=posB.position;
        }

        if (Vector2.Distance(transform.position, posB.position) < 0.05f)
        {
            tranGetPots = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position,tranGetPots, speed * Time.deltaTime);

        

    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D col) //當人物站上平台會跟著移動
    {
        
        if (col.collider.tag=="Gameplayer")
        {
            col.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D col) //人物離開平台則不受限制
    {
        if (col.collider.tag == "Gameplayer")
        {
            col.transform.parent = null;
        }
    }

    public void Pause(float duration)
    {
        isPaused = true;
        pauseTimer = duration;
    }

}
