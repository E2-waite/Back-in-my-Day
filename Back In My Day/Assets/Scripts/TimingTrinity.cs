using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingTrinity : MonoBehaviour
{
    IEnumerator Timings()
    {
        yield return new WaitForSeconds(5);
        // Music fade in
        // Lights turn on (with spotlight clunk)
        // Music particles start
        yield return new WaitForSeconds(3);
        // Dancing particles fade in
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
