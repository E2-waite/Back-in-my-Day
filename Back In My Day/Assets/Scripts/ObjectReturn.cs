using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturn : MonoBehaviour
{
    GameObject glow_obj;
    Renderer glow;
    OVRGrabbable grabbable;
    Vector3 start_pos;
    Quaternion start_rot;
    bool picked_up = false;
    StartTransition transition;
    void Start()
    {
        if (transform.childCount > 0)
        {
            glow_obj = transform.GetChild(0).gameObject;
            glow = glow_obj.GetComponent<Renderer>();
        }
        if (GetComponent<OVRGrabbable>() != null)
        {
            Debug.Log("GOT GRABBABLE");
            grabbable = GetComponent<OVRGrabbable>();
        }
        else
        {
            Debug.Log("NO GRABBABLE");
        }
        start_pos = transform.position;
        start_rot = transform.rotation;
        transition = GetComponent<StartTransition>();
    }

    private void Update()
    {
        if (grabbable.isGrabbed && !picked_up)
        {        
            picked_up = true;
            transition.Transition();
            transform.position = transform.position;
            transform.rotation = transform.rotation;
        }
        if (!grabbable.isGrabbed && picked_up)
        {
            picked_up = false;
            transition.ResetTransition();
            transform.position = start_pos;
            transform.rotation = start_rot;
        }

        if (grabbable.isGrabbed)
        {
            glow.enabled = false;
        }
        else
        {
            glow.enabled = true;
        }
    }
}
