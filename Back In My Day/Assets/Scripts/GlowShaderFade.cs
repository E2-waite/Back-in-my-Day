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
        StartCoroutine(FadeGradient(Direction.up));
    }
    enum Direction
    {
        up,
        down
    };
    IEnumerator FadeGradient(Direction dir)
    {
       if (dir == Direction.up)
       {
            float value = 0;
            while (value < 1)
            {
                value += rate * Time.deltaTime;
                material.SetFloat("_GradientPos", value);
                yield return null;
            }
            StartCoroutine(FadeGradient(Direction.down));
       }
       else
       {
            float value = 1;
            while (value > 0)
            {
                value -= rate * Time.deltaTime;
                material.SetFloat("_GradientPos", value);
                yield return null;
            }
            StartCoroutine(FadeGradient(Direction.up));
        }
    }
}
