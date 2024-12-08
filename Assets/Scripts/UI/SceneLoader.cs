using UnityEngine;
using UnityEngine.SceneManagement; // �Ω�[������
using UnityEngine.UI; // �Ω�B�z���s�I���ƥ�

public class SceneLoader : MonoBehaviour
{
    public string sceneName;  // �n�[���������W��
    public Button loadButton; // UI ���s

    void Start()
    {
        // ����s�Q�I���ɡAĲ�o LoadScene ��k
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadScene); // ���U���s�I���ƥ�
        }
    }

    // �[�����w������
    void LoadScene()
    {
        SceneManager.LoadScene(sceneName); // �ھڳ����W�٥[������
    }
}
