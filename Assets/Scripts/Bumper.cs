using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]

public class Bumper : MonoBehaviour
{
    
    public Text myText;


    [SerializeField] float power = 1f;
    Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        //myText.text = "0";
    }

    void OnCollisionEnter(Collision col)
    {
        Score.instance.AddScore(500);
        //myText.text = (Score.instance.ReadScore()).ToString();

        print("Score: " + Score.instance.ReadScore());

        foreach(ContactPoint contact in col.contacts)
        {
            contact.otherCollider.attachedRigidbody.AddForce(-1 * contact.normal * power, ForceMode.Impulse);  
        }

        if(anim != null)
        {
            anim.SetTrigger("activate");
        }
    }

}
