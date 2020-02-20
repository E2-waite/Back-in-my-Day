using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenes : MonoBehaviour
{
    public GameObject player;
    public GameObject screen_cover;
    ScreenFade fade;
    public List<string> scene_names = new List<string>();
    List<Scene> scenes = new List<Scene>();
    public List<GameObject> scene_obj = new List<GameObject>();
    SCENES current_scene = SCENES.Menu;

    enum SCENES
    {
        Trinity,
        Fleece,
        Lakota,
        Cosies,
        Menu
    }

    void Start()
    {
        fade = screen_cover.GetComponent<ScreenFade>();
        StartCoroutine(SetupScenes());
    }

    IEnumerator SetupScenes()
    {
        for (int i = 0; i < scene_names.Count; i++)
        {
            SetupScene(scene_names[i]);
            yield return null;
        }
        for (int i = 0; i < scene_names.Count; i++)
        {
            SetupObject(scene_names[i]);
            yield return null;
        }
    }

    void SetupScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        scenes.Add(SceneManager.GetSceneByName(scene));
    }

    void SetupObject(string scene)
    {
        scene_obj.Add(GameObject.Find(scene));
        if (scene != "Menu")
        {
            scene_obj[scene_obj.Count - 1].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp("1") && !fade.fading)
        {
            if (current_scene == SCENES.Trinity)
            {
                StartCoroutine(ActivateScene(4));
                current_scene = SCENES.Menu;
            }
            else
            {
                StartCoroutine(ActivateScene(0));
                current_scene = SCENES.Trinity;
            }
        }
        if (Input.GetKeyUp("2") && !fade.fading)
        {
            if (current_scene == SCENES.Fleece)
            {
                StartCoroutine(ActivateScene(4));
                current_scene = SCENES.Menu;
            }
            else
            {
                StartCoroutine(ActivateScene(1));
                current_scene = SCENES.Fleece;
            }
        }
        if (Input.GetKeyUp("3") && !fade.fading)
        {
            if (current_scene == SCENES.Lakota)
            {
                StartCoroutine(ActivateScene(4));
                current_scene = SCENES.Menu;
            }
            else
            {
                StartCoroutine(ActivateScene(2));
                current_scene = SCENES.Lakota;
            }
        }
        if (Input.GetKeyUp("4") && !fade.fading)
        {
            if (current_scene == SCENES.Cosies)
            {
                StartCoroutine(ActivateScene(4));
                current_scene = SCENES.Menu;
            }
            else
            {
                StartCoroutine(ActivateScene(3));
                current_scene = SCENES.Cosies;
            }
        }
    }

    void DisableAll()
    {
        for (int i = 0; i < scene_obj.Count; i++)
        {   
            scene_obj[i].SetActive(false);
        }
    }

    IEnumerator ActivateScene(int num)
    {
        StartCoroutine(fade.FadeIn());
        yield return null;
        while (fade.fading) { yield return null; }
        DisableAll();
        scene_obj[num].SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(fade.FadeOut());
    }
    IEnumerator DeactivateScene(int num)
    {
        scene_obj[num].SetActive(false);
        yield return null;
    }
}
