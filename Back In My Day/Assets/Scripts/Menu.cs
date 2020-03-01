using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    Scenes scene_manager;

    public void SetupSceneManager(GameObject manager)
    {
        scene_manager = manager.GetComponent<Scenes>();
    }

    public Scenes Scenes()
    {
        return scene_manager;
    }
}
