using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    public Animator AniTime;
    public GameObject NextObj;
    public bool ToNextSceen;
    public string NextSceenName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (AniTime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f) {
            if (ToNextSceen)
            {
                Application.LoadLevel(NextSceenName);
            }
            else
            {
                gameObject.SetActive(false);
                NextObj.SetActive(true);
            }
        }
    }
}
