using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLadder : MonoBehaviour
{
    public GameObject Ladder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D hit)
    {
        if (hit.CompareTag("Player")) {
            Ladder.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            // Ladder.SetActive(false);
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(0.1f);
        Ladder.SetActive(false);
    }
}
