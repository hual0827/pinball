using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{

    public Vector3 pos;

    public int count;

    static public float leftFootY;
    static public float rightFootY;
    static public float leftFootZ;
    static public float rightFootZ;
    
    static public float leftFoot2Y;
    static public float rightFoot2Y;
    static public float leftFoot2Z;
    static public float rightFoot2Z;

    public Material BoneMaterial;
    public GameObject BodySourceManager;

    public GameObject pinball;
    public GameObject leftFlip;
    public GameObject rightFlip;

    public bool pow;
    public bool power = true;
    
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };
    
    void Start()
    {
        pinball.SetActive(false);   
        //print("Off: " + pinball.activeSelf);

    }

    void Update () 
    {

        //key controls for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pinball.SetActive(true);
            pinball.transform.position = new Vector3((float)-0.62, (float)4.38, (float)-0.61);   
        }
        if (Input.GetKeyDown(KeyCode.A))
        { 
            Vector3 newRotation = new Vector3(-178, 1, 40);
            leftFlip.transform.eulerAngles = newRotation; 
            pow = true;
        }   
        if (Input.GetKeyDown(KeyCode.D))
        { 
            Vector3 newRotation = new Vector3(0, 0, -7);
            rightFlip.transform.eulerAngles = newRotation;
            pow = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Vector3 backRotation = new Vector3(-178, 1, 70);
            leftFlip.transform.eulerAngles = backRotation; 
            pow = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Vector3 backRotation = new Vector3(0, 0, 23);
            rightFlip.transform.eulerAngles = backRotation; 
            pow = false;
        }

                

        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();

        
        if (data == null)
        {
            return;
        }

        //if (power){     
        //    foreach (var body in data){
                
        //        count++;
                
        //    }
        //    power = false;
        //}
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
                
            if(body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        if (pinball.activeSelf)
        {
            Rigidbody rb = pinball.GetComponent<Rigidbody>();
            Vector3 vel = rb.velocity;

            float x = vel[0];
            float z = vel[2];
        }
        
        Rigidbody lrb = leftFlip.GetComponent<Rigidbody>();
        Rigidbody rrb = rightFlip.GetComponent<Rigidbody>();
        
       


        float rnd = Random.Range(0f, 0.5f);
       
       if (pinball.activeSelf)
       {
            pos = pinball.transform.position;
       }
        
        bool foundLeft = false;
        bool foundRight = false;

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                    //print(body.TrackingId);
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);
    
                Kinect.Joint footLeft = body.Joints[Kinect.JointType.FootLeft];
                Kinect.Joint footRight = body.Joints[Kinect.JointType.FootRight];
                Kinect.Joint handRight = body.Joints[Kinect.JointType.HandRight];

                //print(handRight.Position.Y);

                if (footLeft.Position.X < 0){
                    
                    foundLeft = true;

                    leftFootY = footLeft.Position.Y;
                    rightFootY = footRight.Position.Y;

                    leftFootZ = footLeft.Position.Z;
                    rightFootZ = footRight.Position.Z;

                    print("Left1: " + leftFootZ + "Right1: " + rightFootZ);

                }
                else if (footLeft.Position.X > 0){
                    // && footLeft.Position.X > -1 && footLeft.Position.Z < 2
                    foundRight = true;

                    leftFoot2Y = footLeft.Position.Y;
                    rightFoot2Y = footRight.Position.Y;

                    leftFoot2Z = footLeft.Position.Z;
                    rightFoot2Z = footRight.Position.Z;

                    print("Left2: " + leftFoot2Z + "Right2: " + rightFoot2Z);
                }                

                //launch ball
                //if ((leftFootY > 0 && rightFootY > 0) || (leftFoot2Y > 0 && rightFoot2Y > 0))
                //if (Input.GetKeyDown(KeyCode.Space))
                if (handRight.Position.Y > 0)
                {
                    pinball.SetActive(true);
                    //print("On: " + pinball.activeSelf);
                    pinball.transform.position = new Vector3((float)-0.62, (float)4.38, (float)-0.61);   
                }
     
                //player one controls left flipper
                else if (leftFootZ > rightFootZ + 0.2 || rightFootZ > leftFootZ + 0.2)
                    { 
                  
                        Vector3 newRotation = new Vector3(0, 0, 10);
                        leftFlip.transform.eulerAngles = newRotation; 

                        pow = true;
                       
                    }
                
                //player two controls right flipper
                else if (rightFoot2Z > leftFoot2Z + 0.2 || leftFoot2Z > rightFoot2Z + 0.2)
                    { 
                  
                        Vector3 newRotation = new Vector3(0, 0, -10);
                        rightFlip.transform.eulerAngles = newRotation;

                        pow = true;
              
                    }
                
                else if (footRight.Position.Y < footLeft.Position.Y + 0.2 && footRight.Position.Y > footLeft.Position.Y - 0.2 && leftFlip.transform.rotation.z > 0) {
                    Vector3 backRotation = new Vector3(0, 0, -13);
                    leftFlip.transform.eulerAngles = backRotation;

                    pow = false;
                
                }
                 else if (footRight.Position.Y < footLeft.Position.Y + 0.2 && footRight.Position.Y > footLeft.Position.Y - 0.2 && rightFlip.transform.rotation.z < 0) {
                    Vector3 backRRotation = new Vector3(0, 0, 13);
                    rightFlip.transform.eulerAngles = backRRotation;

                    pow = false;
                }
            
               
            }
        }
        if (!foundLeft){
            leftFootY = 0;
            leftFootZ = 0;
        }
        if (!foundRight){
            leftFoot2Y = 0;
            leftFoot2Y = 0;
        }
    }

   
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.05f, 0.05f);
            
            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
