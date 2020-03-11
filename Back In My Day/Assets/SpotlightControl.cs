using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightControl : MonoBehaviour
{
    public Transform look_pos;
    public GameObject spotlight_stand;
    public GameObject spotlight_obj;
    public GameObject light_obj;
    public Material light_on;
    public Material light_off;
    public float rotateSpeed = 1;
    bool active = false;
    Quaternion stand_start;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            LookAtPoint(look_pos);
        }
        else
        {
            // Look at start point
            //LookAtPoint()
        }
    }

    void LookAtPoint(Transform target)
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        //Vector3 difference = target.position - transform.position;
        //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //spotlight_obj.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }


    public void Toggle()
    {
        if (active)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    void TurnOn()
    {
        light_obj.SetActive(true);
        active = true;
    }

    void TurnOff()
    {
        light_obj.SetActive(false);
        active = false;
    }
}
