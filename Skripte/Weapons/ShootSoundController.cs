using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSoundController : MonoBehaviour
{

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShot()
    {
        audioSource.Play();
    }
}
