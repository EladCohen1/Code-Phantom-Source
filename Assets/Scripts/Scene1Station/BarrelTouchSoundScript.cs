using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelTouchSoundScript : MonoBehaviour
{
    [SerializeField] AudioSource barrelTouchSound;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            barrelTouchSound.Play();
        }
    }
}
