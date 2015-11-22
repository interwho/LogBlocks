using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float speed = 10;
	float xRotation;
	
	void Start() {
		xRotation = transform.rotation.x;
	}
	
	void Update() {
		OVRManager.DismissHSWDisplay();
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			Strafe(speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
		{
			Strafe(-speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey (KeyCode.UpArrow))
		{
			Fly(speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey (KeyCode.DownArrow))
		{
			Fly(-speed * Time.deltaTime);
		}

		float dx = Input.GetAxis("Mouse X");
		float dy = Input.GetAxis("Mouse Y");
		xRotation = transform.rotation.x;
		transform.position = new Vector3(transform.position.x, Mathf.Max(3, transform.position.y), transform.position.z);
	}
	
	void Strafe(float dist) {
		transform.Translate(Vector3.left * dist);
	}
	
	void Fly(float dist) {
		transform.Translate(Vector3.forward * dist);
	}
	
	void Look(Vector3 dist) {
		Vector3 angles = transform.eulerAngles;
		angles += new Vector3(-dist.y, dist.x, dist.z);
		transform.eulerAngles = new Vector3(ClampAngle(angles.x), angles.y, angles.z);
	}
	
	float ClampAngle(float angle) {
		if (angle < 180f)
		{
			if (angle > 80f) angle = 80f;
		}
		else
		{
			if (angle < 280f) angle = 280f;
		}
		return angle;
	}
}
