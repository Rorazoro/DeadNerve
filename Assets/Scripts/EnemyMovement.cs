using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyMovement : NetworkBehaviour {
    private Enemy enemy;
    private NavMeshAgent Nav;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
        enemy = GetComponent<Enemy>();
		enemy.Target = GameObject.FindGameObjectWithTag("Player") != null ? GameObject.FindGameObjectWithTag("Player").transform : null;
		Nav = GetComponent<NavMeshAgent>();
	}

    /// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (enemy.Target != null) {
			Nav.SetDestination(enemy.Target.position);
		}
		else {
			enemy.Target = GameObject.FindGameObjectWithTag("Player") != null ? GameObject.FindGameObjectWithTag("Player").transform : null;
		}
	}
}
