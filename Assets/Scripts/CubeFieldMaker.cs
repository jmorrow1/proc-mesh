using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFieldMaker : MonoBehaviour 
{
    public CubeMaker[,] cubes;
    public GameObject cubePrefab;
    public int cubesPerWidth = 20;
    public int cubesPerDepth = 20;
    public float separationPercent = 0.2f; // percentage of a cube that separates the cubes
    public Vector3 cubeSize = Vector3.one * 0.5f;
    public float startX = 0, yPlane = 0, startZ = 0;

	void Start () 
	{
        Vector3 separationDist = cubeSize + cubeSize * (separationPercent);

        cubes = new CubeMaker[cubesPerWidth, cubesPerDepth];

        float x = startX;
		for (int i=0; i<cubesPerWidth; i++)
        {
            float z = startZ;
            for (int j=0; j<cubesPerDepth; j++)
            {
                GameObject cube = Instantiate(cubePrefab, this.transform);
                cube.transform.position = new Vector3(x, yPlane, z);
                CubeMaker cubeMaker = cube.GetComponent<CubeMaker>();
                cubeMaker.size = cubeSize;

                cubes[i, j] = cubeMaker;

                z += separationDist.z;
            }
            x += separationDist.x;
        }
	}
}
