using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class LogConversation : MonoBehaviour
{
    public GameObject UICanvas, DialogueObj;
    //對話一開啟
    public void OnConversationStart(Transform actor)
    {
        Debug.Log(string.Format("Starting conversation with {0}", actor.name));
    }
    //對話中
    public void OnConversationLine(Subtitle subtitle)
    {
        Debug.Log(string.Format("{0}: {1}", subtitle.speakerInfo.transform.name, subtitle.formattedText.text));
    }
    //對話結束
    public void OnConversationEnd(Transform actor)
    {
        Debug.Log(string.Format("Ending conversation with {0}", actor.name));
        UICanvas.SetActive(true);
        DialogueObj.SetActive(false);
    }
}


