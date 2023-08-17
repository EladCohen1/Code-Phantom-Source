using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMusicScript : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        GameObject endBackgroundMusic = GameObject.Find("EndBackgroundMusic");
        if (endBackgroundMusic == null)
        {
            backgroundMusic.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
