using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
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
	private Behaviour[] DisableOnDeath;
	private bool[] WasEnabled;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	public void Setup()
	{
		WasEnabled = new bool[DisableOnDeath.Length];
		WasEnabled = DisableOnDeath.Select(x => x.enabled).ToArray();
		SetDefaults();
	}

	public void SetDefaults() {
		IsDead = false;
		currentHealth = maxHealth;
		for (int i = 0; i < DisableOnDeath.Length; i++)
		{
			DisableOnDeath[i].enabled = WasEnabled[i];
		}
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
		DisableOnDeath.ToList().ForEach(x => x.enabled = false);
		Debug.Log(transform.name + " is DEAD!");
	}
}
