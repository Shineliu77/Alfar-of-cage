using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPrompt : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject hit;
    public Transform player;
    private bool canPress;

    private void Update()
    {
        hit.GetComponent<SpriteRenderer>().enabled = canPress;
        hit.transform.localScale = player.localScale;
    }

    private void OnTriggerEnter2D(Collider2D hitCheck)
    {
        if (hitCheck.CompareTag("SkillUse"))
        {
            canPress = true;
        }
    }
    private void OnTriggerExit2D(Collider2D hitCheck)
    {
        canPress = false;
    }
}
