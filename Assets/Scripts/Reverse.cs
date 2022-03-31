using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{

    public BodySourceView b;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
        float x = vel[0];
        float z = vel[2];
        float rnd = Random.Range(0f, 0.5f);

        print("1");
        print(b.pow);

        if (b.pow == true)
        {
            print("2");
            if (col.gameObject.name == "left flipper")
            {
                rb.velocity = new Vector3(x * -1 + rnd, 13, z);
                print("3");
            }
            if (col.gameObject.name == "right flipper")
            {
                rb.velocity = new Vector3(x * -1 + rnd, 13, z);
                print("3");
            }
        }
    }
}
