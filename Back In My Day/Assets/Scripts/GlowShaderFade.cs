using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowShaderFade : MonoBehaviour
{
    public float rate = 0.1f;
    Renderer[] renderers;
    private void Start()
    {
        renderers = gameObject.GetComponentsInChildren<Renderer>();
    }

    public void SetAlpha(float val)
    {
        if (val <= 0.5f)
        {
            foreach (Renderer rend in renderers)
            {
                rend.material.SetFloat("_Alpha", val);
            }
        }
    }
}
