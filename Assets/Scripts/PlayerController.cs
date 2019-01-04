using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed;
	Rigidbody rb;
	Vector3 moveDirection;
	CapsuleCollider col;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	private statsinfo statsinfo;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		statsinfo = GetComponent<statsinfo>();
	}

	// Update is called once per frame
	void Update () {
		float horizontalMovement = 0;
		float verticaleMovement = 0;

		if (CanMove(transform.right * Input.GetAxisRaw("Horizontal")))
		{
			horizontalMovement = Input.GetAxisRaw("Horizontal");
		}

		if (CanMove(transform.forward * Input.GetAxisRaw("Vertical")))
		{
			verticaleMovement = Input.GetAxisRaw("Vertical");
		}

		moveDirection = (horizontalMovement * transform.right + verticaleMovement * transform.forward).normalized;

	}

	void FixedUpdate () {

		Move();
		Fire();


	}

	void Move() {

		Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);
		rb.velocity = moveDirection * walkSpeed * Time.deltaTime;
		rb.velocity += yVelFix;

	}

	bool CanMove(Vector3 dir) {
		float distanceToPoints = col.height / 2 - col.radius;

		Vector3 point1 = transform.position + col.center + Vector3.up * distanceToPoints;
		Vector3 point2 = transform.position + col.center - Vector3.up * distanceToPoints;

		float radius = col.radius * 0.95f;
		float castDistance = 0.5f;

		RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, dir, castDistance);

		foreach (RaycastHit objectHit in hits)
		{
			if (objectHit.transform.tag == "Wall")
			{
				return false;
			}
		}
		 return true;
	}

	void Fire() 
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			var bullet = Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 16;

			var bulletStats = bullet.GetComponent<Bullets>();
			bulletStats.bulletDamage = statsinfo.actualDamage;
			Destroy(bullet, 2f);
		}

	}

}
