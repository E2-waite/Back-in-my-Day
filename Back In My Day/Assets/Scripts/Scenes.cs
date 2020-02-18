using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenes : MonoBehaviour
{
    SceneManager manager;
    bool scene_loaded = false;
    Scene menu_scene;
    Scene trinity;
    Scene fleece;
    Scene lakota;
    Scene cosies;
    Scene current_scene;
    // Start is called before the first frame update
    void Start()
    {
        menu_scene = SceneManager.GetActiveScene();
        current_scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Trinity", LoadSceneMode.Additive);
        SceneManager.LoadScene("Fleece", LoadSceneMode.Additive);
        SceneManager.LoadScene("Lakota", LoadSceneMode.Additive);
        SceneManager.LoadScene("Cosies", LoadSceneMode.Additive);
        trinity = SceneManager.GetSceneByName("Trinity");
        fleece = SceneManager.GetSceneByName("Fleece");
        lakota = SceneManager.GetSceneByName("Lakota");
        cosies = SceneManager.GetSceneByName("Cosies");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("1") && !scene_loaded)
        {
            if (current_scene.name == "SampleScene")
            {
                SceneManager.SetActiveScene(trinity);
                current_scene = SceneManager.GetActiveScene();
            }
            else if (current_scene.name == "Trinity")
            {
                SceneManager.SetActiveScene(menu_scene);
                current_scene = SceneManager.GetActiveScene();
            }
        }
    }
}
