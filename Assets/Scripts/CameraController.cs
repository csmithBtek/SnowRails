using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float minX = -60f;
	public float maxX = 60f;
	public float minY = -360f;
	public float maxY = 360;

	public float senX = 15f;
	public float senY = 15f;

	float rotY = 0f;
	float rotX = 0f;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;

	}

	// Update is called once per frame
	void Update () {

		rotY += Input.GetAxis("Mouse X") * senY;
		rotX += Input.GetAxis("Mouse Y") * senX;

		rotX = Mathf.Clamp(rotX, minX, maxX);
		transform.localEulerAngles = new Vector3(0, rotY, 0);


	}
}
