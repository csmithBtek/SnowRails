using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour {

	private statsinfo statsinfo;

	public RectTransform healthBar;
	public bool killonDeath;


	// Use this for initialization
	void Start () {

		statsinfo = GetComponent<statsinfo>();
		healthBar.sizeDelta = new Vector2(100, healthBar.sizeDelta.y);

	}

	public void takeDmg(int amount)
	{
		statsinfo.Health -= amount;
		healthChange();
		if (statsinfo.Health <= 0)
		{
			Destroy(gameObject);
			
		}

	}

	void healthChange ()
	{
		float healthBarSize = ((float)statsinfo.Health / (float)statsinfo.maxHealth) * 100.0f;
		healthBar.sizeDelta = new Vector2(healthBarSize, healthBar.sizeDelta.y);
	}
}
