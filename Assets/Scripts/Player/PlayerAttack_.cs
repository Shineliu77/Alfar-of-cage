using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_ : MonoBehaviour
{

    public GameObject magicMissilePrefab;   // �]�k���u�w�s����
    public Transform firePoint;             // �o�g�I
    public float missileSpeed = 10f;        // ���u�t��
    public float attackRange = 10f;         // �����Z��
    public float attackDamage = 5f;         // �����ˮ`

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // �k����U�ɵo�g���u
        {
            FireMagicMissile();
        }
    }

    void FireMagicMissile()
    {

        // �p�⪱�a��ƹ�����V
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // �Ыح��u�ó]�m���l��m�P��V
        GameObject missile = Instantiate(magicMissilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * missileSpeed;

        // �]�m���u�t�שM��V
        rb.velocity = direction * missileSpeed;

        // �]�w���u�������ˮ`�P�̤j����Z��
        MagicMissile missileScript = missile.GetComponent<MagicMissile>();
        missileScript.SetDamage(attackDamage);
        missileScript.SetRange(attackRange);
    }
}
