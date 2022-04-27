using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundCount : MonoBehaviour
{
    public BodySourceView b;

    void OnTriggerEnter(Collider other)
    {
        b.round++;
        b.fallen = true;
    }
}
