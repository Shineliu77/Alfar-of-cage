using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSkill : MonoBehaviour
{
    public float skillRadius = 5f; // �ޯ�d��b�|
    public LayerMask waterLayer; // ���h
    public GameObject icePrefab; // �B���w�s����]�Ω�a�����^
    public GameObject hailPrefab; // �B�r�w�s����]�Ω�w�����^
    public float hailDamage = 5f; // �B�r�y�����ˮ`
    public string targetTag = "TargetObject"; // ���w���󪺼���

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // �� Q �I��ޯ�
        {
            FreezeWaterInRange();
            DisableTargetsInRange();
        }
    }

    void FreezeWaterInRange()
    {
        // ���o�����m�A���ˬd�d�򤺩Ҧ�����
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius, waterLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // �p�G�O�a���W����
            if (hitCollider.CompareTag("GroundWater"))
            {
                FreezeGroundWater(hitCollider);
            }
            // �p�G�O�w������
            else if (hitCollider.CompareTag("FallingWater"))
            {
                FreezeFallingWater(hitCollider);
            }
        }
    }

    void DisableTargetsInRange()
    {
        // �˴��d�򤺩Ҧ��I����
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, skillRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // �p�G�I���쪺�O���w������A�h�����Ӫ���
            if (hitCollider.CompareTag(targetTag))
            {
                hitCollider.gameObject.SetActive(false);
            }
        }
    }

    void FreezeGroundWater(Collider2D groundWater)
    {
        // ���æa�����A����ܦB��
        groundWater.gameObject.SetActive(false);
        GameObject ice = Instantiate(icePrefab, groundWater.transform.position, Quaternion.identity);
        ice.transform.localScale = groundWater.transform.localScale; // �]�w�B���j�p�P���ۦP
        ice.transform.position = new Vector3(groundWater.transform.position.x, groundWater.transform.position.y, 0);
    }

    void FreezeFallingWater(Collider2D fallingWater)
    {
        fallingWater.gameObject.SetActive(false);
        GameObject hail = Instantiate(hailPrefab, fallingWater.transform.position, Quaternion.identity);

        // �T�w�B�r���j�p�� (0.1, 0.1, 1)
        hail.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        hail.transform.position = new Vector3(fallingWater.transform.position.x, fallingWater.transform.position.y, 0);

        // ���B�r�K�[��ĤH�ˮ`���޿�
        HailDamage hailDamageScript = hail.AddComponent<HailDamage>();
        hailDamageScript.SetDamage(hailDamage);
    }
}
