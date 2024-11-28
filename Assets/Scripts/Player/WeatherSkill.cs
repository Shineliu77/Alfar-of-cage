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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // �� Q �I��ޯ�
        {
            FreezeWaterInRange();
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

    void FreezeGroundWater(Collider2D groundWater)
    {
        // ���æa�����A����ܦB��
        groundWater.gameObject.SetActive(false);
        GameObject ice = Instantiate(icePrefab, groundWater.transform.position, Quaternion.identity);
        ice.transform.localScale = groundWater.transform.localScale; // �]�w�B���j�p�P���ۦP
    }

    void FreezeFallingWater(Collider2D fallingWater)
    {
        // ���úw�����A����ܦB�r
        fallingWater.gameObject.SetActive(false);
        GameObject hail = Instantiate(hailPrefab, fallingWater.transform.position, Quaternion.identity);
        hail.transform.localScale = fallingWater.transform.localScale; // �]�w�B�r�j�p�P�w���ۦP

        // ���B�r�K�[��ĤH�ˮ`���޿�
        HailDamage hailDamageScript = hail.AddComponent<HailDamage>();
        hailDamageScript.SetDamage(hailDamage);
    }

  
}
