using UnityEngine;

public class TriggerObjectScript : MonoBehaviour
{
    public Vector3 targetPosition;   // �]�m���w���ؼЦ�m
    public float triggerRadius = 2f; // �P�_�d��A�i�H�վ�ӳ]�mĲ�o�d��
    public GameObject objectToEnable; // �n�ҥΪ�����

    private bool hasTriggered = false; // �ΨӽT�O�uĲ�o�@��

    void Update()
    {
        // �p�⪱�a�P�ؼЦ�m���Z��
        float distance = Vector3.Distance(transform.position, targetPosition);

        // �ˬd���a�O�_��F���w�d��
        if (distance <= triggerRadius && !hasTriggered)
        {
            Debug.Log("Player reached target position.");

            // �}�ҫ��w������
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }

            hasTriggered = true; // �T�O�uĲ�o�@��
        }
    }

}
