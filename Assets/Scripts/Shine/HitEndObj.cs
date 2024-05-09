using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEndObj : MonoBehaviour
{
    public GameObject EndAni;
    public GameObject PlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<Collider2D>().tag == "Player") {
            EndAni.SetActive(true);
            PlayerCamera.SetActive(false);
        }
    }
}
