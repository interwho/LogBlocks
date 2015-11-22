using UnityEngine;
using System.Collections;

public class CubeCreator : MonoBehaviour {

	string[] endpoints = {"users", "authentication", "logout", "posts"};
	void Start () {
		for (int i=0; i<4; i++) {
			GameObject goodResponseCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			GameObject badResponseCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

			Renderer goodRenderer = goodResponseCube.GetComponentInChildren<Renderer>();
			Renderer badRenderer = badResponseCube.GetComponentInChildren<Renderer>();

			goodResponseCube.transform.position = new Vector3(10, 0, i*20);
			goodResponseCube.transform.localScale = new Vector3(goodResponseCube.transform.localScale.x+5, 1, goodResponseCube.transform.localScale.z+5);
			goodResponseCube.transform.name = endpoints[i];
			CubeGrower cubeGrower = (CubeGrower)goodResponseCube.AddComponent<CubeGrower>();
			cubeGrower.responseCode = 200;
			goodRenderer.material.color = new Color(0f,1f,0f,0f);

			badResponseCube.transform.position = new Vector3(20, 0, i*20);
			badResponseCube.transform.localScale = new Vector3(badResponseCube.transform.localScale.x+5, 1, badResponseCube.transform.localScale.z+5);
			badResponseCube.transform.name = endpoints[i];
			CubeGrower cubeGrower2 = (CubeGrower)badResponseCube.AddComponent<CubeGrower>();
			cubeGrower2.responseCode = 500;

			badRenderer.material.color = new Color(1f,0f,0f,0f);
		}

		Camera.main.transform.position = new Vector3(25, 10, -40);
	}

	void Update () {
	
	}
}
