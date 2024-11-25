using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    public string[] Dialogues;
    int i = 0;
    public Text DialogueText;
    public GameObject NextObj,Player,BlackScreenObj, DialoguesObj;
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
            DialoguesObj.SetActive(false);
            NextObj.SetActive(true);
            Player.SetActive(false);
            StartCoroutine(Wait());
        }
        i = Mathf.Clamp(i, 0, Dialogues.Length);
        DialogueText.text = Dialogues[i];
       
    }
    IEnumerator Wait() { 
    yield return new WaitForSeconds(0.5f);
        BlackScreenObj.SetActive(true);

    }
}
