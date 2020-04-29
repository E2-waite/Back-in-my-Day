using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq.Expressions;

public class Timing : MonoBehaviour
{
    public AudioSource music, spotlight_audio, narration;
    public List<GameObject> spotlights = new List<GameObject>();
    public float music_fade_speed = 2;
    public GameObject dynamic_lights;
    public GameObject dancer_parent;
    public GameObject lazers;
    List<GameObject> dancers = new List<GameObject>();
    bool lights_active = false;
    Scenes scene_manager;
    private void Start()
    {
        foreach (Transform child in dancer_parent.transform)
        {
            dancers.Add(child.gameObject);
        }
    }

    public void StartScene(GameObject scenes, SCENES scene)
    {
        scene_manager = scenes.GetComponent<Scenes>();
        StartCoroutine(Timings());
    }

    IEnumerator Timings()
    {
        Debug.Log("STARTED TIMING");
        //Starts in darkness
        yield return new WaitForSeconds(4);
        // Lights turn on (with spotlight clunk)
        ToggleLights();
        yield return new WaitForSeconds(2);
        // Music fade in & music particles start
        StartCoroutine(FadeMusic(Fade._in));
        yield return new WaitForSeconds(5);
        // Dancing particles fade in
        StartCoroutine(FadeOverTime(5, Fade._in, 0.8f));
        StartCoroutine(HalfFadeMusic(Fade._out));
        yield return new WaitForSeconds(narration.clip.length);
        StartCoroutine(HalfFadeMusic(Fade._in));
        yield return new WaitForSeconds(15);
        // Dancing particles fade out
        // Music fades out
        StartCoroutine(FadeOverTime(5, Fade._out, 0.8f));
        StartCoroutine(FadeMusic(Fade._out));
        yield return new WaitForSeconds(5);
        // Lights turn off
        ToggleLights();
        yield return new WaitForSeconds(4);
        // Return to menu scene
        scene_manager.ToMenu();
    }

    enum Fade
    {
        _in,
        _out
    };

    void ToggleLights()
    {
        spotlight_audio.Play();
        Light[] lights = dynamic_lights.GetComponentsInChildren<Light>();
        if (lights_active)
        {
            if (lazers != null)
            {
                lazers.SetActive(false);
            }
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
            foreach (GameObject spotlight in spotlights)
            {
                MeshRenderer spot_mesh = spotlight.GetComponent<MeshRenderer>();
                spot_mesh.enabled = false;
                spotlight.GetComponent<SpotlightController>().Toggle();
            }
            lights_active = false;
        }
        else
        {

            foreach (Light light in lights)
            {
                light.enabled = true;
            }
            foreach (GameObject spotlight in spotlights)
            {
                MeshRenderer spot_mesh = spotlight.GetComponent<MeshRenderer>();
                spot_mesh.enabled = true;
                spotlight.GetComponent<SpotlightController>().Toggle();
            }
            lights_active = true;
        }
    }

    IEnumerator FadeOverTime(float time, Fade fade, float opacity = 0.8f)
    {
        bool fading = true;
        if (fade == Fade._in)
        {
            if (lazers != null)
            {
                lazers.SetActive(true);
            }
            float current = 0;
            while (fading)
            {
                current += Time.deltaTime;
                float alpha = Mathf.Lerp(0, opacity, current / time);
                foreach (GameObject dancer in dancers)
                {
                    if (dancer.GetComponent<GlowShaderFade>() != null)
                    {
                        GlowShaderFade fader = dancer.GetComponent<GlowShaderFade>();
                        fader.SetAlpha(current);
                    }
                }
                if (alpha >= opacity)
                {
                    fading = false;
                }
                yield return null;
            }
        }
        else
        {
            if (lazers != null)
            {
                lazers.SetActive(false);
            }
            float current = time;
            while (fading)
            {
                current -= Time.deltaTime;
                float alpha = Mathf.Lerp(opacity, 0, current / time);
                foreach (GameObject dancer in dancers)
                {
                    GlowShaderFade fader = dancer.GetComponent<GlowShaderFade>();
                    fader.SetAlpha(current);
                }
                if (alpha <= 0)
                {
                    fading = false;
                }
                yield return null;
            }
        }
    }

    IEnumerator FadeMusic(Fade fade)
    {
        float volume;
        if (fade == Fade._in)
        {
            music.Play();
            volume = 0;
            music.time = 49f;
            // turn on speaker particles
            while (volume < 1)
            {
                volume += (music_fade_speed / 10) * Time.deltaTime;
                music.volume = volume;
                yield return null;
            }
        }
        if (fade == Fade._out)
        {
            volume = 1;
            while (volume > 0)
            {
                volume -= (music_fade_speed / 10) * Time.deltaTime;
                music.volume = volume;
                yield return null;
            }
            // turn off speaker particles
        }
    }

    IEnumerator HalfFadeMusic(Fade fade)
    {
        float volume;
        if (fade == Fade._in)
        {
            volume = music.volume;
            while (volume < 1)
            {
                volume += (music_fade_speed / 10) * Time.deltaTime;
                music.volume = volume;
                yield return null;
            }
        }
        else
        {
            volume = music.volume;
            while (volume > 0.2f)
            {
                volume -= (music_fade_speed / 10) * Time.deltaTime;
                music.volume = volume;
                yield return null;
            }
        }
    }
}