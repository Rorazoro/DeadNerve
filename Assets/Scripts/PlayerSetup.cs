using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[AddComponentMenu("Player/Player Setup")]
[RequireComponent(typeof(Player))]
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
			AssignRemoteLayer();
		}
		else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive(false);
			}

			playerUIInstance = Instantiate(playerUIPrefab);
			playerUIInstance.name = playerUIPrefab.name;
		}

		GetComponent<Player>().Setup();
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

		GameManager.UnRegisterPlayer(transform.name);
	}

	public override void OnStartClient() {
		base.OnStartClient();

		string netID = GetComponent<NetworkIdentity>().netId.ToString();
		Player player = GetComponent<Player>();

		GameManager.RegisterPlayer(netID, player);
	}

	private void DisableComponents() {
		disabledComponents.ToList().ForEach(x => x.enabled = false);
	}

	private void AssignRemoteLayer() {
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}
}
