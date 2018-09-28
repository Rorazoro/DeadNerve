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
			if (hit.collider.tag == "Enemy") {
				//CmdPlayerShot(hit.collider.name, weapon.damage);
				CmdEnemyShot(hit.collider.name, weapon.damage);
			}
		}
	}

	[Command]
	private void CmdPlayerShot(string ID, int damage) {
		Debug.Log(ID + " has been shot.");

		Player player = GameManager.GetPlayer(ID);
		player.RpcTakeDamage(damage);
	}

	[Command]
	private void CmdEnemyShot(string ID, int damage) {
		Debug.Log(ID + " has been shot.");

		Enemy enemy = GameManager.GetEnemy(ID);
		enemy.RpcTakeDamage(damage);
	}
}
