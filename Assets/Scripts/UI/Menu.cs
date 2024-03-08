using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider ControlVolumSlider;
    public AudioListener AudioListenerObj;
    public Dropdown GameSizeDropdown;
    void Start()
    {
        AudioListener.volume = ControlVolumSlider.value;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetMusicVolume()
    {
        AudioListener.volume = ControlVolumSlider.value;
        SaveData.SaveVolume = AudioListener.volume ;
    }

    public void SetGameSize() 
    {
    if (GameSizeDropdown.value == 0)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        if (GameSizeDropdown.value == 1)
        {
            Screen.SetResolution(1280, 720, false);
        }
        if (GameSizeDropdown.value == 2)
        {
            Screen.SetResolution(800, 400, false);
        }
    }
}
