using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timing : MonoBehaviour
{
    public AudioSource music, spotlight_audio;
    public List<GameObject> spotlights = new List<GameObject>();
    public float music_fade_speed = 2;
    public List<ParticleSystem> PSList = new List<ParticleSystem>();
    public GameObject dynamic_lights;
    private GameObject dancerPrefab;
    public List<GameObject> dancers = new List<GameObject>();
    bool lights_active = false;
    private void Start()
    {
        //this.gameObject = dancerPrefab;

        foreach (GameObject Actor in GameObject.FindGameObjectsWithTag("Dancer"))
        {
            dancers.Add(Actor);
        }
        foreach (GameObject Actor in dancers)
        {
            //Actor.GetComponentsInChildren<ParticleSystem>();
            PSList.AddRange(Actor.GetComponentsInChildren<ParticleSystem>());

        }
        foreach (ParticleSystem ps in PSList)
        {
            var emission = ps.emission;
            emission.rateOverTime = 0;
        }
        StartScene();
    }

    public void StartScene()
    {
        StartCoroutine(Timings());
    }

    IEnumerator Timings()
    {
        //Starts in darkness
        yield return new WaitForSeconds(4);
        // Lights turn on (with spotlight clunk)
        ToggleLights();
        yield return new WaitForSeconds(2);
        // Music fade in & music particles start
        StartCoroutine(FadeMusic(Fade._in));
        yield return new WaitForSeconds(5);
        // Dancing particles fade in
        StartCoroutine(FadeParticles(Fade._in, 5));
        yield return new WaitForSeconds(45);
        // Dancing particles fade out
        // Music fades out
        StartCoroutine(FadeParticles(Fade._out, 5));
        StartCoroutine(FadeMusic(Fade._out));
        yield return new WaitForSeconds(5);
        // Lights turn off
        ToggleLights();
        yield return new WaitForSeconds(4);
        // Return to menu scene
    }

    IEnumerator FadeParticles(Fade fade, float over_time)
    {
        if (fade == Fade._in)
        {
            float t = 0, rate;
            while (t < 1)
            {
                rate = Mathf.Lerp(0, 70000, t);
                t += Time.deltaTime / over_time;

                foreach (ParticleSystem ps in PSList)
                {
                    var emission = ps.emission;
                    emission.rateOverTime = rate;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            float t = 1, rate;
            while (t > 0)
            {
                rate = Mathf.Lerp(0, 70000, t);
                t -= Time.deltaTime / over_time;

                foreach (ParticleSystem ps in PSList)
                {
                    var emission = ps.emission;
                    emission.rateOverTime = rate;
                }
                yield return new WaitForEndOfFrame();
            }
        }
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
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
            lights_active = false;
        }
        else
        {
            foreach (Light light in lights)
            {
                light.enabled = true;
            }
            lights_active = true;
        }

        //for (int i = 0; i < spotlights.Count; i++)
        //{
        //    spotlights[i].GetComponent<SpotlightControl>().Toggle();
        //}
    }


    IEnumerator FadeMusic(Fade fade)
    {
        float volume;
        if (fade == Fade._in)
        {
            music.Play();
            volume = 0;
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
}
