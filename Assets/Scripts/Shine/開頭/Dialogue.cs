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
    public Camera mainCamera;   // �D��v��
    public AudioSource audioSource; // ���W����
       public float shakeDuration = 0.5f; // ���Y�_�ʫ���ɶ�
    public float shakeMagnitude = 0.1f; // ���Y�_�ʱj��
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
        if (i == Dialogues.Length) // �p�G�����ܧ�
        {
            DialoguesObj.SetActive(false); // ���ù�ܮ�
            if (NextObj != null) {
                NextObj.SetActive(true);
            }
            StartCoroutine(CameraShake()); // �}�l���Y�_��
        }
        else
        {
            i = Mathf.Clamp(i, 0, Dialogues.Length); // ������޽d��
            DialogueText.text = Dialogues[i]; // ��s��ܤ�r
        }
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPosition = mainCamera.transform.localPosition; // �O�s��v����l��m
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // �p��s���H����m
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            mainCamera.transform.localPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y + offsetY,
                originalPosition.z
            );

            elapsedTime += Time.deltaTime; // �W�[�g�L�ɶ�
            yield return null; // ���ݤU�@�V
        }

        mainCamera.transform.localPosition = originalPosition; // ��_��v����l��m

        audioSource.Play();


        StartCoroutine(Wait()); // �_�ʵ�����Ұ� Wait ��{
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f); // ����0.5��
        BlackScreenObj.SetActive(true); // �ҥζ«̪���
    }
}
