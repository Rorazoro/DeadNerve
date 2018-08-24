using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[AddComponentMenu("Player/Player Setup")]
public class PlayerSetup : NetworkBehaviour {
	[SerializeField]
	Behaviour[] disabledComponents;

	Camera sceneCamera;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		if (!isLocalPlayer) {
			foreach (Behaviour b in disabledComponents)
			{
				b.enabled = false;
			}
		}
		else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive(false);
			}
		}
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive(true);
		}
	}
}
