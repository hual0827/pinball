using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]

public class flip : MonoBehaviour
{
    [SerializeField] float startPos = 0;
    [SerializeField] float endPos = 45;
    [SerializeField] float power = 10;
    [SerializeField] float damper = 1;

    public BodySourceView b;

    HingeJoint joint;
    JointSpring spring;
    JointLimits limits;

    public enum Sides
    {
        LEFT,
        RIGHT
    }
    public Sides side;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
         // FLIPPERS 2.0 *********************************************************
        
        joint = GetComponent<HingeJoint>();
        //SPRING
        joint.useSpring = true;
        spring = new JointSpring();
        spring.spring = power;
        spring.damper = damper;

        //LIMITS
        joint.useLimits = true;
        limits = new JointLimits();
        limits.min = startPos;
        limits.max = endPos * direction;
        joint.limits = limits;

        // FLIPPERS 2.0 **********************************************************
    }

    // Update is called once per frame
    void Update()
    {
         // FLIPPERS 2.0 **********************************************************

        if(side == Sides.LEFT || b.pow)
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {    
                spring.targetPosition = endPos * direction;
            }
            else
            {
                spring.targetPosition = startPos;
            }
        }
        if(side == Sides.RIGHT || b.pow2)
        {
            if(Input.GetKey(KeyCode.RightArrow))
            {
                spring.targetPosition = endPos * direction;
            }
            else
            {
                spring.targetPosition = startPos;
            }
        }
        joint.spring = spring;

        // FLIPPERS 2.0 **********************************************************
    }
}
