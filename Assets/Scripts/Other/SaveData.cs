using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static float SaveVolume;
    public float Test;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = SaveData.SaveVolume;
        Test = SaveData.SaveVolume;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
