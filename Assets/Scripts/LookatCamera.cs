using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamera : MonoBehaviour {

	public int directions = 4;
	public bool MirrorLeft = true;
	Animator ani;
	SpriteRenderer spriteRend;
	float minMirrorAngle = 0;
	float maxMirrorAngle = 0;
	public Camera lookHere;

	// Use this for initialization
	void Start () {
		ani = this.GetComponent<Animator>();
		spriteRend = this.GetComponent<SpriteRenderer>();
		if (directions <= 0)
		{
			directions = 1;
		}
		minMirrorAngle = (360 / directions) / 2;
		maxMirrorAngle = 180 - minMirrorAngle;
		
	}

	private void Awake()
	{
		if (lookHere == null)
		{
			lookHere = Camera.main;
		}

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 viewDirection = -new Vector3(lookHere.transform.forward.x, 0, lookHere.transform.forward.z);
		transform.LookAt(transform.position + viewDirection);
		ani.SetFloat("ViewAngle", transform.localEulerAngles.y);
		if (MirrorLeft)
		{
			spriteRend.flipX = !(transform.localEulerAngles.y >= minMirrorAngle && transform.localEulerAngles.y <= maxMirrorAngle);
		}


		
	}
}
