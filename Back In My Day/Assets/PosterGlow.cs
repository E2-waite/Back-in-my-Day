using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterGlow : MonoBehaviour
{
    GameObject glow_obj;
    OVRGrabbable grabbable;
    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        glow_obj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
