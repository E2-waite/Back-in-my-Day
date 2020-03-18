using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLightmap : MonoBehaviour
{
    LightmapControl lightmap;
    // Start is called before the first frame update
    void Start()
    {
        lightmap = GetComponent<LightmapControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1"))
        {
            lightmap.Load("TrinityDark");
        }
        if (Input.GetKey("2"))
        {
            lightmap.Load("TrinityLight");
        }
    }
}
