using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
	[SerializeField]
	private float speed = 5f;

	private PlayerMotor motor;

	public void Start() {
		motor = GetComponent<PlayerMotor>();
	}

	public void Update() {
		float _xMov = Input.GetAxisRaw("Horizontal");
		float _zMov = Input.GetAxisRaw("Vertical");

		Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;

		Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

		motor.Move(_velocity);
	}
}
