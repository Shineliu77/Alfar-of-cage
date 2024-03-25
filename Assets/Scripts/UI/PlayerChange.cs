using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    [Header("Time")]
    public GameObject Time;
    public GameObject Tskill;
    private bool TimeOpen;

    [Header("Weather")]
    public GameObject Weather;
    public GameObject Wskill;
    private bool WeatherOpen;
    

    [Header("Speace")]
    public GameObject Speace;
    public GameObject Sskill;
    private bool SpeaceOpen;

    private void Awake()
    {
        TimeOpen = true;
    }
    public void Update()
    {
        Time.SetActive(TimeOpen);
        Tskill.SetActive(TimeOpen);

        Weather.SetActive(WeatherOpen);
        Wskill.SetActive(WeatherOpen);

        Speace.SetActive(SpeaceOpen);
        Sskill.SetActive(SpeaceOpen);
    }
    public void timeopen()
    {
        //Debug.Log("Time");
        TimeOpen = true;
        WeatherOpen = false;
        SpeaceOpen = false;
    }
    public void Weatheropen()
    {
        //Debug.Log("weather");
        TimeOpen = false;
        WeatherOpen = true;
        SpeaceOpen = false;
    }
    public void Speaceopen()
    {
        //Debug.Log("speace");
        TimeOpen = false;
        WeatherOpen = false;
        SpeaceOpen = true;
    }

}
