using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/Player Motor")]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;

	private Vector3 _velocity = Vector3.zero;
	private Vector3 _rotation = Vector3.zero;
	private Vector3 _cameraRotation = Vector3.zero;

	private Rigidbody rb;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		rb = GetComponent<Rigidbody>();	
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}

	public void Move(Vector3 velocity) {
		_velocity = velocity;
	}

	public void Rotate(Vector3 rotation) {
		_rotation = rotation;
	}

	public void RotateCamera(Vector3 rotation) {
		_cameraRotation = rotation;
	}

	private void PerformMovement() {
		if (_velocity != Vector3.zero) {
			rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
		}
	}

	private void PerformRotation() {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(_rotation));
		if (cam != null) {
			cam.transform.Rotate(-_cameraRotation);
		}
	}
}
