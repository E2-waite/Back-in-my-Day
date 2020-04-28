using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    public Renderer rend;
    bool active = false;
    public void Toggle()
    {
        if (active)
        {
            active = false;
            rend.materials[1].SetFloat("_State", 0);
        }
        else
        {
            active = true;
            rend.materials[1].SetFloat("_State", 1);
        }
    }
}
