using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Light))]
public class VolumetricLight : MonoBehaviour
{

    public float max_opacity = 0.25f;

    private MeshFilter filter;
    private Light main_light;
    private Mesh mesh;

    void Start()
    {
        filter = GetComponent<MeshFilter>();
        main_light = GetComponent<Light>();
        if (main_light.type != LightType.Spot)
        {
            Debug.LogError("ATTACHED VOLUMETRIC LIGHT MESH TO NON-SUPPORTED LIGHT TYPE (USE SPOTLIGHT)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        mesh = BuildMesh();
        filter.mesh = mesh;
    }

    private Mesh BuildMesh()
    {
        Mesh mesh = new Mesh();

        float farPosition = Mathf.Tan(main_light.spotAngle * 0.5f * Mathf.Deg2Rad) * main_light.range;
        mesh.vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(farPosition,farPosition,main_light.range),
            new Vector3(-farPosition,farPosition, main_light.range),
            new Vector3(-farPosition,-farPosition, main_light.range),
            new Vector3(farPosition,-farPosition, main_light.range)
        };
        mesh.colors = new Color[]
        {
            new Color(main_light.color.r, main_light.color.g, main_light.color.b, main_light.color.a * max_opacity),
            new Color(main_light.color.r, main_light.color.g, main_light.color.b, 0),
            new Color(main_light.color.r, main_light.color.g, main_light.color.b, 0),
            new Color(main_light.color.r, main_light.color.g, main_light.color.b, 0),
            new Color(main_light.color.r, main_light.color.g, main_light.color.b, 0)
        };
        mesh.triangles = new int[]
        {
            0,1,2,
            0,2,3,
            0,3,4,
            0,4,1
        };
        return mesh;
    }
}
