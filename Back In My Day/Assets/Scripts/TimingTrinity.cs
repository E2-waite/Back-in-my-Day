using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingTrinity : MonoBehaviour
{
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
        // Lights turn on (with spotlight clunk)
        // Music particles start
        //yield return new WaitForSeconds(3);
        // Dancing particles fade in
        //foreach (ParticleSystem ps in PSList)
        //{
        //
        //    var emission = ps.emission;
        //    emission.rate = Mathf.Lerp(0, 70000, t);
        //    t += Time.deltaTime / 3;
        //    yield return new WaitForEndOfFrame();
        //}


        //var emission = ps.emission;
        while (t < 1)
        {
            rate = Mathf.Lerp(0, 70000, t);
            t += Time.deltaTime / 10;
            yield return new WaitForEndOfFrame();
        }
        //t = 0;
        

        yield return new WaitForSeconds(40);
        // Dancing particles fade out
        yield return new WaitForSeconds(3);
        // Music fades out
        // Lights turn off
        // Music particles stop
        yield return new WaitForSeconds(5);
        // Return to menu scene
    }
}
