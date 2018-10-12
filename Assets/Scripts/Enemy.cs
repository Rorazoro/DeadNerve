using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {
	[SyncVar]
	private bool _isDead = false;
	public bool IsDead {
		get { return _isDead; }
		protected set { _isDead = value; }
	}

	[SerializeField]
	private int maxHealth = 100;
	[SerializeField][SyncVar] 
	private int currentHealth;

	[SerializeField]
	public Transform Target;
	public EnemySpawner spawner;

	private void Awake() {
		SetDefaults();
	}

	public void SetDefaults() {
		IsDead = false;
		currentHealth = maxHealth;
	}

	[ClientRpc]
	public void RpcTakeDamage(int amount) {
		currentHealth -= amount;
		Debug.Log(transform.transform.name + " now has " + currentHealth + " health.");
		if (currentHealth <= 0) {
			Die();
		}
	}
	private void Die() {
		IsDead = true;
		Debug.Log(transform.name + " is DEAD!");
		spawner.currentNumberOfEnemies--;
		Destroy(gameObject);
	}
}
