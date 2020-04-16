using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowShaderFade : MonoBehaviour
{
    public float rate = 0.1f;
    List<Material> materials = new List<Material>();
    public Renderer rend;
    private void Start()
    {
        foreach(Transform child in transform)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                materials.Add(child.GetComponent<Renderer>().material);
            }
        }
    }

    public void SetAlpha(float val)
    {
        foreach (Material mat in materials)
        {
            mat.SetFloat("_Alpha", val);
        }
    }
}
