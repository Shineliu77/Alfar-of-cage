using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 50f;
    private float currentHealth;

    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage,Vector3 dir)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        ApplyKnockBack(dir);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ¾P·´¼Ä¤H
        Destroy(gameObject);
        Debug.Log("BL Alfar died.");
    }


    [Header("À»°h¶ZÂ÷"), SerializeField] private float distance;
    [Header("À»°h®É¶¡"), SerializeField] private float time;
   
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float timer;
    private bool isKnockBack;

    public void ApplyKnockBack(Vector3 dir)
    {
        isKnockBack = true;
        Debug.Log(dir);
        //Vector3 direction = faceDir == 1 ? Vector3.left : Vector3.right;

        // if (faceDir == 1)
        // {
        //     direction = Vector3.left;
        // }
        // else
        // {
        //     direction = Vector3.right;
        // }

        startPosition = transform.position;
        endPosition = startPosition + dir * distance;
        transform.position = endPosition;
    }

    private void Update()
    {
        if (isKnockBack)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, timer / time);

            if (timer >= time)
            {
                isKnockBack = false;
                timer = 0;
            }
        }
    }
}
