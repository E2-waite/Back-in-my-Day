using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    Text text;
    public Color opaque;
    public Color transparent;
    public float fade_time = 2, fade_wait = 5, fade_delay = 5;
    float fade_progress = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.color = Color.Lerp(transparent, opaque, Mathf.Clamp(fade_progress, 0, 1));
    }

    private void OnEnable()
    {
        fade_progress = 0;
        StartCoroutine(FadeLoop());
    }

    IEnumerator FadeLoop()
    {
        yield return new WaitForSeconds(fade_wait);
        float current_time = 0;
        while (current_time < fade_time)
        {
            current_time += Time.deltaTime;
            fade_progress = current_time / fade_time;
            yield return null;
        }
        yield return new WaitForSeconds(fade_delay);
        
        current_time = fade_time;
        while (current_time > 0)
        {
            current_time -= Time.deltaTime;
            fade_progress = current_time / fade_time;
            yield return null;
        }
        StartCoroutine(FadeLoop());
    }
}
