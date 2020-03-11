using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingTrinity : MonoBehaviour
{
    public AudioSource music, spotlight_audio;
    public List<GameObject> spotlights = new List<GameObject>();
    public float music_fade_speed = 2;
    public List<ParticleSystem> PSList = new List<ParticleSystem>();

    private GameObject dancerPrefab;
    private List<GameObject> dancers = new List<GameObject>();

    public float x = 5000.0f;

    private void Start()
    {
        //this.gameObject = dancerPrefab;
        
        foreach(GameObject Actor in GameObject.FindGameObjectsWithTag("Dancer"))
        {
            dancers.Add(Actor);
        }
        foreach(GameObject Actor in dancers)
        {
            //Actor.GetComponentsInChildren<ParticleSystem>();
            PSList.AddRange(Actor.GetComponentsInChildren<ParticleSystem>());
        }

      
        StartCoroutine(Timings());
    }

    float t, rate;

    private void Update()
    {
        foreach (ParticleSystem ps in PSList)
        {
            var emission = ps.emission;
            emission.rateOverTime = rate;
        }
    }

    IEnumerator Timings()
    {
        //yield return new WaitForSeconds(2);
        //var emission = ps.emission;
        //emission.rateOverTime = x;

        //yield return new WaitForSeconds(5);
        // Music fade in
        //StartCoroutine(FadeMusic(Fade._in));
        // Lights turn on (with spotlight clunk)
        //ToggleSpotlights();
        // Music particles start
        //yield return new WaitForSeconds(3);
        // Dancing particles fade in

        while (t < 1)
        {
            rate = Mathf.Lerp(0, 70000, t);
            t += Time.deltaTime / 10;
            yield return new WaitForEndOfFrame();
        }
        

        yield return new WaitForSeconds(40);
        // Dancing particles fade out
        yield return new WaitForSeconds(3);
        // Music fades out
        //StartCoroutine(FadeMusic(Fade._out));
        // Lights turn off
        //ToggleSpotlights();
        // Music particles stop
        yield return new WaitForSeconds(5);
        // Return to menu scene
    }

    enum Fade
    {
        _in,
        _out
    };

    void ToggleSpotlights()
    {
        //spotlight_audio.Play();
        for (int i = 0; i < spotlights.Count; i++)
        {
            spotlights[i].GetComponent<SpotlightControl>().Toggle();
        }
    }


    IEnumerator FadeMusic(Fade fade)
    {
        float volume;
        if (fade == Fade._in)
        {
            volume = 0;
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
        }
    }
}
