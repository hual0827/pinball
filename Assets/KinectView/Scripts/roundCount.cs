using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundCount : MonoBehaviour
{
    public BodySourceView b;
    public AudioSource gameOver;

    void OnTriggerEnter(Collider other)
    {
        if(b.round == 3)
        {
            gameOver.Play();
        }
        b.round++;
        b.fallen = true;
    }
}
