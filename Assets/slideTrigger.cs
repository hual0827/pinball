using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class slideTrigger : MonoBehaviour
{

    public AudioSource slideSound;


    private void OnTriggerEnter(Collider other)
    {
        if (slideSound.isPlaying == false)
        {
            slideSound.Play();
        }
    }
}
