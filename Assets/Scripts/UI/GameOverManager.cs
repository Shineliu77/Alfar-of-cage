using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // �Ѧ� Game Over �� Panel
    public float delayBeforeGameOver = 1f; // ����ɶ�

    void Start()
    {
        // ���� Game Over ���O
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        // ������� Game Over �e��
        Invoke("DisplayGameOver", delayBeforeGameOver);
    }

    private void DisplayGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // ���s���J���e����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        // ���J�D�������]�T�O�D�������W�٥��T�^
        SceneManager.LoadScene("Menu");
    }
}