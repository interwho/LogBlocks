using UnityEngine;
using System.Collections;

public class CubeGrower : MonoBehaviour {

	int updateCounter=0;
	int height;
	GameObject textObj;
	TextMesh textMesh;
	public int responseCode;

	void Start () {
		textObj = new GameObject();
		textMesh = (TextMesh)textObj.AddComponent<TextMesh>();
		MeshRenderer meshRenderer = (MeshRenderer)textObj.AddComponent<MeshRenderer>();
		textMesh.fontSize = 14;
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		if (www.error != null) {
			return false;
		}
		height = int.Parse(www.data);
		transform.localScale = new Vector3(transform.localScale.x, 2*height*0.1f, transform.localScale.z);
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}

	void Update () {
		if (updateCounter < 50) {
			updateCounter++;
			return;
		}
		WWW www = new WWW(string.Format("45.79.134.56/frontend/logsearch?route=/api/{0}&status={1}", transform.name, responseCode));
		StartCoroutine(WaitForRequest(www));
		updateCounter = 0;
		textMesh.text = string.Format ("Endpoint: {0}, Number of {1}'s: {2}", transform.name, responseCode, height);
		int offset = responseCode == 200 ? -35 : 5;
		textObj.transform.position = new Vector3(transform.position.x+offset, 0, transform.position.z);
	}
}
