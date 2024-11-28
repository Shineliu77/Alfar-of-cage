using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineCheck : MonoBehaviour
{

    PlayableDirector director;
    [Header("°Êµe¬í¼Æ")]
    public float SetTime;
    public GameObject DialogueObj;
    public GameObject AniObj;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }
    private void Update()
    {
        if (director.time >= SetTime)
        {
            DialogueObj.SetActive(true);
            AniObj.SetActive(false);
        }

    }
}