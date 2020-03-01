using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class StartTransition : MonoBehaviour
{
    float transition_time = 2;
    public SCENES scene;
    Menu menu;

    private void Start()
    {
        menu = GameObject.Find("Menu").GetComponent<Menu>();
    }

    public void ResetTransition()
    {
        StopAllCoroutines();
    }

    public void Transition()
    {
        StartCoroutine(TransitionRoutine());
    }

    IEnumerator TransitionRoutine()
    {
        yield return new WaitForSeconds(transition_time);
        menu.Scenes().TransitionScene(scene);
    }
}
