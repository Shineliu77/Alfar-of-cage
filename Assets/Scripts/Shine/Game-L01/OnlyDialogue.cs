using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlyDialogue : MonoBehaviour
{

    public string[] Dialogues;
    int i = 0;
    public Text DialogueText;
    public GameObject NextObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickNext() {
        i++;
        if (i == Dialogues.Length)
        {
            gameObject.SetActive(false);
            if (NextObj != null)
            {
                NextObj.SetActive(true);
            }
        }
        i = Mathf.Clamp(i, 0, Dialogues.Length);
        DialogueText.text = Dialogues[i];
       
    }
   
}
