using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeatherSkill : MonoBehaviour
{
    public GameObject icePrefab;     // ���B�᪺����w�s����
    public float freezeRange = 5f;   // �ޯ�d��]���GUnity���^

    void Update()
    {
        // ���U Q ��ϥΧޯ�
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill();
        }
    }

    void UseSkill()
    {
        Debug.Log("����ޯ�ҰʡG���B�d�򤺪����I");
        FreezeAllWaterInRange();
    }

    void FreezeAllWaterInRange()
    {
        // �����������Ҧ� GroundWater
        GameObject[] groundWaters = GameObject.FindGameObjectsWithTag("GroundWater");
        foreach (GameObject water in groundWaters)
        {
            if (IsWithinRange(water.transform.position))
            {
                FreezeGroundWater(water);
            }
        }

        // �����������Ҧ� FallingWater
        GameObject[] fallingWaters = GameObject.FindGameObjectsWithTag("FallingWater");
        foreach (GameObject water in fallingWaters)
        {
            if (IsWithinRange(water.transform.position))
            {
                FreezeFallingWater(water);
            }
        }
    }

    bool IsWithinRange(Vector3 position)
    {
        // �ˬd�ؼЦ�m�O�_�b�d��
        return Vector3.Distance(transform.position, position) <= freezeRange;
    }

    void FreezeGroundWater(GameObject water)
    {
        // �������B��
        Instantiate(icePrefab, water.transform.position, Quaternion.identity);
        Destroy(water); // �R����
    }

    void FreezeFallingWater(GameObject water)
    {
        // �������B��
        Instantiate(icePrefab, water.transform.position, Quaternion.identity);
        Destroy(water); // �R����
    }
}
