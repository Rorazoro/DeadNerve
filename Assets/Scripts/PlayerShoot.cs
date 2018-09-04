using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[AddComponentMenu("Player/Player Shoot")]
public class PlayerShoot : NetworkBehaviour {

	public Weapon weapon;

	[SerializeField]
	private Camera cam;
	[SerializeField]
	private LayerMask mask;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		if (cam == null) {
			Debug.LogError("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (Input.GetButtonDown("Fire1")) {
			Shoot();
		}
	}

	[Client]
	private void Shoot() {
		RaycastHit hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask)) {
			if (hit.collider.tag == "Player") {
				CmdPlayerShot(hit.collider.name);
			}
		}
	}

	[Command]
	private void CmdPlayerShot(string ID) {
		Debug.Log(ID + " has been shot.");
	}
}
