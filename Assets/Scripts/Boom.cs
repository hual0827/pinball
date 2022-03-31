using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boom : MonoBehaviour
{
    //public float MoveSpeed = -0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isCollided)
        {
            //print("You lose");
        }
    }
    
    private bool isCollided = false;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
            isCollided = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
            isCollided = false;  
    }
}