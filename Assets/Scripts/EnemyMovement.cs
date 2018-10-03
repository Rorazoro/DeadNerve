using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyMovement : NetworkBehaviour {
    private Enemy enemy;
	private EnemySight sight;
    private NavMeshAgent Nav;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
        enemy = GetComponent<Enemy>();
		sight = GetComponent<EnemySight>();
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
			if (sight.visibleTargets.Count > 0) {
				enemy.Target = GetClosestTarget(sight.visibleTargets);
			}
			//enemy.Target = GameObject.FindGameObjectWithTag("Player") != null ? GameObject.FindGameObjectWithTag("Player").transform : null;
		}
	}

	Transform GetClosestTarget(List<Transform> targets)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in targets)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
