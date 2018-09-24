using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySight))]
public class FieldOfViewEditor : Editor {

	private void OnSceneGUI() {
		EnemySight sight = (EnemySight)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(sight.transform.position, Vector3.up, Vector3.forward, 360, sight.viewRadius);
		Vector3 viewingAngleA = sight.DirFromAngle(-sight.viewAngle / 2, false);
		Vector3 viewingAngleB = sight.DirFromAngle(sight.viewAngle / 2, false);

		Handles.DrawLine(sight.transform.position, sight.transform.position + viewingAngleA * sight.viewRadius);
		Handles.DrawLine(sight.transform.position, sight.transform.position + viewingAngleB * sight.viewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in sight.visibleTargets)
		{
			Handles.DrawLine(sight.transform.position, visibleTarget.position);
		}
	}

}
