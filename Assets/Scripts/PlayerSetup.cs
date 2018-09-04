using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[AddComponentMenu("Player/Player Setup")]
public class PlayerSetup : NetworkBehaviour {
	[SerializeField]
	Behaviour[] disabledComponents;
	[SerializeField]
	string remoteLayerName = "RemotePlayer";
	[SerializeField]
	GameObject playerUIPrefab;
	private GameObject playerUIInstance;

	Camera sceneCamera;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		if (!isLocalPlayer) {
			DisableComponents();
		}
		else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive(false);
			}

			playerUIInstance = Instantiate(playerUIPrefab);
			playerUIInstance.name = playerUIPrefab.name;
		}

		RegisterPlayer();
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		Destroy(playerUIInstance);

		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive(true);
		}
	}
	
	private void DisableComponents() {
		foreach (Behaviour b in disabledComponents)
			{
				b.enabled = false;
			}
	}

	private void AssignRemoteLayer() {
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	private void RegisterPlayer() {
		string ID = "Player " + GetComponent<NetworkIdentity>().netId;
		transform.name = ID;
	}
}
