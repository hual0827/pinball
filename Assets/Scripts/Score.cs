﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(score);
       
    }
     public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Ball")
                score+=500;
        }
}
