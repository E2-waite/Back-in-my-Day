using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;
public class Scenes : MonoBehaviour
{
    public GameObject player;
    public GameObject screen_cover;
    ScreenFade fade;
    public List<string> scene_names = new List<string>();
    List<Scene> scenes = new List<Scene>();
    public List<GameObject> scene_obj = new List<GameObject>();
    SCENES current_scene = SCENES.Menu;
    Posters posters;
    void Start()
    {
        fade = screen_cover.GetComponent<ScreenFade>();
        StartCoroutine(SetupScenes());
    }

    private void Update()
    {
        if (Input.GetKeyUp("1"))
        {
            TransitionScene(SCENES.Trinity);
        }
        if (Input.GetKeyUp("2"))
        {
            TransitionScene(SCENES.Lakota);
        }
        if (Input.GetKeyUp("3"))
        {
            TransitionScene(SCENES.Fleece);
        }
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
        posters = GameObject.Find("Posters").GetComponent<Posters>();
    }

    void SetupScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        scenes.Add(SceneManager.GetSceneByName(scene));
    }

    void SetupObject(string scene)
    {
        scene_obj.Add(GameObject.Find(scene));
        if (scene == "Menu")
        {
            scene_obj[scene_obj.Count - 1].GetComponent<Menu>().SetupSceneManager(this.gameObject);
        }
        else
        {
            scene_obj[scene_obj.Count - 1].SetActive(false);
        }
    }

    public void TransitionScene(SCENES to_scene)
    {
        if (!fade.fading)
        {
            StartCoroutine(ActivateScene((int)to_scene));
            current_scene = to_scene;
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
        while (fade.fading) { yield return null; }
        DisableAll();
        posters.Disable();
        SceneManager.SetActiveScene(scenes[num]);
        scene_obj[num].SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(fade.FadeOut());
        scene_obj[num].GetComponent<Timing>().StartScene(this.gameObject);
    }

    public void ToMenu()
    {
        StartCoroutine(ReturnToMenu());
    }
    IEnumerator ReturnToMenu()
    {
        StartCoroutine(fade.FadeIn());
        while (fade.fading) { yield return null; }
        DisableAll();
        posters.Enable();
        SceneManager.SetActiveScene(scenes[(int)SCENES.Menu]);
        scene_obj[(int)SCENES.Menu].SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(fade.FadeOut());
    }
}
