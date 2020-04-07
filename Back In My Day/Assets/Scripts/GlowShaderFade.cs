using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowShaderFade : MonoBehaviour
{
    public float rate = 0.1f;
    Material material;
    public Renderer rend;
    private void Start()
    {
        material = rend.GetComponent<Renderer>().material;
    }

    public void SetAlpha(float val)
    {
        material.SetFloat("_Alpha", val);
    }
}
