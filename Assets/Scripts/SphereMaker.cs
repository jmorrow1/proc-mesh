using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class SphereMaker : MonoBehaviour 
{

    public int latitudeLineCount;
    public int longitudeLineCount;
    public float radius;
    private MeshFilter meshFilter;

	void Start () 
	{
        meshFilter = GetComponent<MeshFilter>();
	}
	
	void Update () 
	{
        Vector3[] vertices = new Vector3[latitudeLineCount * longitudeLineCount];
        int vtxIndex = 0;

        for (int j = 0; j < latitudeLineCount; j++)
        {
            float lat = 180 * ((float)(j + 1) / latitudeLineCount);

            for (int i = 0; i < longitudeLineCount; i++)
            {
                float lon = 360 * ((float)i / longitudeLineCount);

                Vector3 cartesian = new Vector3(
                    radius * Mathf.Cos(lat) * Mathf.Cos(lon),
                    radius * Mathf.Cos(lat) * Mathf.Sin(lon),
                    radius * Mathf.Sin(lat)
                );

                vertices[vtxIndex++] = cartesian;
            }
        }

        MeshCreator mc = new MeshCreator();
        for (int i=2; i<vertices.Length; i++)
        {
            mc.BuildTriangle(vertices[i - 2], vertices[i - 1], vertices[i]);
        }

        meshFilter.mesh = mc.CreateMesh();
    }
}
