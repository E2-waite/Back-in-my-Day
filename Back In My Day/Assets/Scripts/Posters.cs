using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posters : MonoBehaviour
{
    List<GameObject> posters = new List<GameObject>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            posters.Add(child.gameObject);
        }
    }

    public void Disable()
    {
        for (int i = 0; i < posters.Count; i++)
        {
            posters[i].SetActive(false);
        }
    }

    public void Enable()
    {
        for (int i = 0; i < posters.Count; i++)
        {
            posters[i].SetActive(true);
        }
    }
}
