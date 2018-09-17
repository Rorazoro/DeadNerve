using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {
	[SerializeField]
	private int maxHealth = 100;
	[SerializeField][SyncVar] 
	private int currentHealth;

	[SerializeField]
	public Transform Target;
}
