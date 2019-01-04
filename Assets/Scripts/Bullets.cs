using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    public int bulletDamage;
	public GameObject owner;


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;

        var health = hit.GetComponent<Health>();
        if (health != null && hit != owner)
        {
            health.takeDmg(bulletDamage);
        }

        var bully = hit.GetComponent<Bullets>();
        if (bully == null)
        {
            Destroy(gameObject);
        }

    }
}
