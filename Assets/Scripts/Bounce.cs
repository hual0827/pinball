using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class Bounce : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public Vector3 pos;

   
   
    // Start is called before the first frame update
    void Start()
    {
        
        //pos = transform.position;
        //Debug.Log("pos is " + pos);
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
        float x = vel[0];
        float z = vel[2];


        float rnd = Random.Range(0f, 0.5f);
       
        pos = transform.position;
        

        if (pos[1] < -3 && pos[1] > -4)
        {
          
		
            if (Input.GetKeyDown("space"))
            {              
                rb.velocity = new Vector3(x * -1 + rnd, 13, z);
            }
        }

    }


}
