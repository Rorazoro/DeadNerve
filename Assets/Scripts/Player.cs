using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	[SerializeField]
	private int maxHealth = 100;
	[SerializeField][SyncVar]
	private int currentHealth;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		SetDefaults();
	}

	public void SetDefaults() {
		currentHealth = maxHealth;
	}

	public void TakeDamage(int amount) {
		currentHealth -= amount;
		Debug.Log(transform.transform.name + " now has " + currentHealth + " health.");
	}
}
