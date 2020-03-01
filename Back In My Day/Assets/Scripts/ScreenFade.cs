using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenFade : MonoBehaviour
{
    public Image BlackScreen;
    public float fade_speed = 10;
    public bool fading;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        fading = true;
        var temp_colour = BlackScreen.color;
        while (fading)
        {
            if (temp_colour.a < 1)
            {
                temp_colour.a += fade_speed * Time.deltaTime;
                BlackScreen.color = temp_colour;
            }
            else
            {
                fading = false;
            }
            yield return null;
        }
    }


    public IEnumerator FadeOut()
    {
        fading = true;
        var temp_colour = BlackScreen.color;
        while (fading)
        {
            if (temp_colour.a > 0)
            {
                temp_colour.a -= fade_speed * Time.deltaTime;
                BlackScreen.color = temp_colour;
            }
            else
            {
                fading = false;
            }
            yield return null;
        }
        fading = false;
    }
}
