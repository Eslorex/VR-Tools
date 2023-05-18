using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCamAngle : MonoBehaviour
{
    public Camera VRCam;
    public GameObject Target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CamDirection = VRCam.transform.forward;
        Vector3 TargetPos = Target.transform.position;
        Vector3 Difference = TargetPos - VRCam.transform.position;
        double theta = (Math.Tan( (Vector3.Dot(CamDirection,Difference)) / (CamDirection.magnitude * Difference.magnitude)) * Mathf.Rad2Deg) / 90;
        theta = Mathf.Clamp((float)theta,0,1);
        Debug.Log("Angle is : " + theta);
    }
}
