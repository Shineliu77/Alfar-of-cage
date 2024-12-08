using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icetree : MonoBehaviour 
{

    public GameObject time;
    public GameObject weather;
    public GameObject ice;
    public GameObject dead;
    public GameObject good;
    
    public bool istime;
    public bool isweather;
    public bool canUse;
    public bool isdead;
    public bool isgood;

    public bool playerin;


    // Start is called before the first frame update
    void Start()
    {
        canUse=true;
    }

    // Update is called once per frame
    void Update()
    {
        ice.SetActive(canUse);
        dead.SetActive(isdead);
        good.SetActive(isdead);

        

        /*if(time.enabled)
        {
            istime=false;
        }

        if()
        {
            
        }

        if(playerin==true && Input.GetKeyDown(KeyCode.Q) && canUse==true)
        {
            
        }
         if(playerin==true && Input.GetKeyDown(KeyCode.Q) && isdead==true)
        {

        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerin = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerin = false;
        }
    }
}
