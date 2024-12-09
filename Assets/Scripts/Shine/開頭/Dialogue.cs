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
    public Camera mainCamera;   // 主攝影機
    public AudioSource audioSource; // 音頻播放器
       public float shakeDuration = 0.5f; // 鏡頭震動持續時間
    public float shakeMagnitude = 0.1f; // 鏡頭震動強度
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
        if (i == Dialogues.Length) // 如果對話顯示完
        {
            DialoguesObj.SetActive(false); // 隱藏對話框
            if (NextObj != null) {
                NextObj.SetActive(true);
            }
            StartCoroutine(CameraShake()); // 開始鏡頭震動
        }
        else
        {
            i = Mathf.Clamp(i, 0, Dialogues.Length); // 限制索引範圍
            DialogueText.text = Dialogues[i]; // 更新對話文字
        }
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPosition = mainCamera.transform.localPosition; // 保存攝影機初始位置
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // 計算新的隨機位置
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            mainCamera.transform.localPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y + offsetY,
                originalPosition.z
            );

            elapsedTime += Time.deltaTime; // 增加經過時間
            yield return null; // 等待下一幀
        }

        mainCamera.transform.localPosition = originalPosition; // 恢復攝影機初始位置

        audioSource.Play();


        StartCoroutine(Wait()); // 震動結束後啟動 Wait 協程
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f); // 等待0.5秒
        BlackScreenObj.SetActive(true); // 啟用黑屏物件
    }
}
