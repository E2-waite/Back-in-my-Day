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
    // Start is called before the first frame update


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
        if (scene != "Menu")
        {
            scene_obj.Add(GameObject.Find(scene));
            scene_obj[scene_obj.Count - 1].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp("1") && !fade.fading)
        {
            StartCoroutine(ActivateScene(0));
        }
    }

    IEnumerator ActivateScene(int num)
    {
        StartCoroutine(fade.FadeIn());
        yield return null;
        while (fade.fading) { yield return null; }
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
