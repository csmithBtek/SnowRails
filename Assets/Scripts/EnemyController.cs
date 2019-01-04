using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public GameObject rangeProjectilePrefab;
	public Transform projectileSpawn;

	public float viewRadius = 8f;
	public float atkRange = 8f;
	public SphereCollider test;
	public float wanderRadius;
	public float wanderTime;

	private float timer;

	private bool attacking;

	Transform target;
	NavMeshAgent agent;

	private statsinfo statsinfo;

	enum State {Idle, Attack, Wander, Chase, Escape}
	State state;

	enum Blah {Attack}

	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		statsinfo = GetComponent<statsinfo>();

		timer = wanderTime;
		agent.autoBraking = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		atkRange = test.radius;

		// stuff that happens no matter what state you are in

		switch(state)
		{
			case State.Attack: 
				//UpdateAttack(); 
				break;
			case State.Chase: 
				//UpdateChase(); 
				break;
		}
	
		attacking = false;
		float distance = Vector3.Distance(target.position, transform.position);

		// make function "wander"
		if (distance >= viewRadius)
		{
			timer += Time.deltaTime;
			if (timer >= wanderTime)
			{
				Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
				agent.SetDestination(newPos);
				timer = 0;
			}
		}
	//Debug.Log("dist"+distance);
	//Debug.Log("view"+viewRadius);
	//Debug.Log("atk"+atkRange);
		// make function "chase"
		if (distance <= viewRadius || statsinfo.Health < statsinfo.maxHealth)
		{
			agent.SetDestination(target.position);

			if (distance <= atkRange)
			{
				agent.isStopped = true;

				UpdateRangedAttack();
			}
			else{
				attacking = false;
				agent.isStopped = false;
			}
		}
		///runFromPlayer(atkRange);
		
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, viewRadius);
	}

	public static Vector3 RandomNavSphere (Vector3 origin, float dist, int lmask)
	{
		Vector3 randomDir = Random.insideUnitSphere * dist;
		randomDir += origin;
		NavMeshHit nHit;
		NavMesh.SamplePosition(randomDir, out nHit, dist, lmask);
		return nHit.position;
	}

	void runFromPlayer (float dist)
	{
		float distance = Vector3.Distance(transform.position, target.position);
		if (distance < dist)
		{
			Vector3 dirToPlayer = transform.position - target.position;
			Vector3 newPos = transform.position + dirToPlayer;

			agent.SetDestination(newPos);
		}
		
	}

	void UpdateRangedAttack ()
	{
		float nextAtk = 0f;
		timer += Time.deltaTime;
		if ( timer > statsinfo.atkSpeed)
		{
			nextAtk = timer + statsinfo.atkSpeed;
			Debug.Log(nextAtk + " nxtAtk");
			Debug.Log(Time.time + " time");

            var projectile = Instantiate(rangeProjectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 20;
            var projectileStats = projectile.GetComponent<Bullets>();
            projectileStats.bulletDamage = statsinfo.actualDamage;
            projectileStats.owner = gameObject;
			timer = 0;
			
		}

	}

	void UpdateChase ()
	{
		
	}

	void UpdateIdle ()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
		Debug.Log("now idle");

    }

	// void UpdateWander ()
	// {
	// 	if (distance >= viewRadius)
	// 	{
	// 		timer += Time.deltaTime;
	// 		if (timer >= wanderTime)
	// 		{
	// 			Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
	// 			agent.SetDestination(newPos);
	// 			timer = 0;
	// 		}
	// 	}
	// }
}
