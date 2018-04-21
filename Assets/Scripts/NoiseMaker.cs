using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeFieldMaker))]

public class NoiseMaker : MonoBehaviour 
{
    private CubeMaker[,] cubes;
    private float time;

    public float minHeight;
    public float heightScale = 5;
    public Vector3 noiseScale = Vector3.one * 0.01f;

	void Start () 
	{
        StartCoroutine("LateStart");
	}

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        CubeFieldMaker cfm = GetComponent<CubeFieldMaker>();
        cubes = cfm.cubes;
        minHeight = cfm.cubeSize.y;
    }
	
	void Update () 
	{
        time += Time.deltaTime;

        if (cubes != null)
        {
            float maxHeight = minHeight * heightScale;

            // update cube heights with noise
            Vector3 noiseIn = new Vector3(0, time * noiseScale.y, 0);
            for (int i = 0; i < cubes.GetLength(0); i++)
            {
                noiseIn.z = 0;
                for (int j = 0; j < cubes.GetLength(1); j++)
                {
                    float noiseOut = Perlin.Noise(noiseIn);
                    float cubeHeight = Mathf.Lerp(minHeight, maxHeight, noiseOut);
                    cubes[i, j].size.y = cubeHeight;
                    noiseIn.z += noiseScale.z;
                }
                noiseIn.x += noiseScale.x;
            }
        }

        
	}
}
