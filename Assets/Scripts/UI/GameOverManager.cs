using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // �Ѧ� Game Over �� Panel
    public float delayBeforeGameOver = 0.5f; // ����ɶ�

    [Header("UI ����")]
    public GameObject[] uiElementsToDisable; // �n������ UI �����]�Ҧp����B������s���^

    void Start()
    {
        // ���� Game Over ���O
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        // ���ùC�� UI�]�Ҧp����M������^
        foreach (var uiElement in uiElementsToDisable)
        {
            uiElement.SetActive(false);
        }

      
        gameOverPanel.SetActive(true);
    }

    private void DisplayGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RespawnPlayer()
    {
        // �ھڼ��ҧ�쪱�a����
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            // ���s�ҥΪ��a���󪺴�V�B�I���M���z
            SpriteRenderer[] renderers = playerObject.GetComponentsInChildren<SpriteRenderer>();
            Collider2D[] colliders = playerObject.GetComponentsInChildren<Collider2D>();
            Rigidbody2D[] rigidbodies = playerObject.GetComponentsInChildren<Rigidbody2D>();

            // �ҥδ�V�B�I���M���z
            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
            }

            // �]�m���a��m���̫��ܦ�m
            playerObject.transform.position = TriggerDialogue.lastDialoguePosition;

            // �T�O���a����q�^�_
            PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.playerHealth = playerHealth.maxHealth;  // ���m��q

                // ��s���
                if (playerHealth.healthSlider != null)
                {
                    playerHealth.healthSlider.value = playerHealth.playerHealth;
                }
            }

            Debug.Log("Player respawned via GameOverManager.");
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }

        // ���� Game Over �e��
        gameOverPanel.SetActive(false);

        // ��_ UI ����
        foreach (var uiElement in uiElementsToDisable)
        {
            uiElement.SetActive(true);
        }

        // ��_�C���G�N�ɶ���_���`�]�]�m�� 1�^
        Time.timeScale = 1;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }






}
