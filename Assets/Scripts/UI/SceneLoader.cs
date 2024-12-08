using UnityEngine;
using UnityEngine.SceneManagement; // 用於加載場景
using UnityEngine.UI; // 用於處理按鈕點擊事件

public class SceneLoader : MonoBehaviour
{
    public string sceneName;  // 要加載的場景名稱
    public Button loadButton; // UI 按鈕

    void Start()
    {
        // 當按鈕被點擊時，觸發 LoadScene 方法
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadScene); // 註冊按鈕點擊事件
        }
    }

    // 加載指定的場景
    void LoadScene()
    {
        SceneManager.LoadScene(sceneName); // 根據場景名稱加載場景
    }
}
