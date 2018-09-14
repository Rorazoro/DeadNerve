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
	private Transform Target;

	private NavMeshAgent Nav;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		Target = GameObject.FindGameObjectWithTag("Player") != null ? GameObject.FindGameObjectWithTag("Player").transform : null;
		Nav = GetComponent<NavMeshAgent>();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (Target != null) {
			Nav.SetDestination(Target.position);
		}
	}
}
