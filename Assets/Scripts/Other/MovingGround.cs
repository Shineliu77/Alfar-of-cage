using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{

    public Transform posA, posB;
    public float speed;
    Vector3 tranGetPots;

    private void Start()
    {
        tranGetPots = posB.position;
    }

    private void Update()
    {
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

    private void OnCollisionEnter2D(UnityEngine.Collision2D col)
    {
        //Debug.Log(col.collider.tag );
        if (col.collider.tag=="Gameplayer")
        {
            col.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D col)
    {
        if (col.collider.tag == "Gameplayer")
        {
            col.transform.parent = null;
        }
    }

}
