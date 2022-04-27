using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]

public class Bumper : MonoBehaviour
{
    
    [SerializeField] float power = 1f;

    public AudioSource BumperSound;

    Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        Score.instance.AddScore(500);

        print("Score: " + Score.instance.ReadScore());

        foreach(ContactPoint contact in col.contacts)
        {
            contact.otherCollider.attachedRigidbody.AddForce(-1 * contact.normal * power, ForceMode.Impulse);
            BumperSound.Play();
            print("hit");
        }

        if(anim != null)
        {
            anim.SetTrigger("activate");
        }
    }

}
