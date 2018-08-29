using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/Player Controller")]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
	[SerializeField]
	public float walkSpeed = 5f;
	[SerializeField]
	public float sprintSpeed = 8f;
	[SerializeField]
	public float crouchSpeed = 3f;
	[SerializeField]
	public float lookSensitivity = 5f;
	[SerializeField]
	public float jumpSpeed = 8f;
	[SerializeField]
	public float gravity = 20f;

	private Vector3 _velocity = Vector3.zero;
	private PlayerMotor motor;

	public void Start() {
		motor = GetComponent<PlayerMotor>();
	}

	public void Update() {
		HandleMovementAndJump();
		HandleRotation();
	}

	private void HandleMovementAndJump() {
		float _xMov = Input.GetAxisRaw("Horizontal");
		float _zMov = Input.GetAxisRaw("Vertical");

		if (motor.CharController.isGrounded) {
			_velocity = new Vector3(_xMov, 0, _zMov);
			_velocity = transform.TransformDirection(_velocity);
			if (Input.GetButton("Fire3")) {
				_velocity *= sprintSpeed;
			}
			else if (Input.GetButton("Fire1")) {
				_velocity *= crouchSpeed;
			}
			else {
				_velocity *= walkSpeed;
			}
			
			if (Input.GetButtonDown("Jump")) {
				_velocity.y = jumpSpeed;
			}
		}
		else {
			_velocity = new Vector3(_xMov, _velocity.y, _zMov);
			_velocity = transform.TransformDirection(_velocity);
			_velocity.x *= walkSpeed;
			_velocity.z *= walkSpeed;
		}

		_velocity.y -= gravity * Time.deltaTime;
		motor.Move(_velocity);
	}

	private void HandleRotation() {
		//Rotates Player Left/Right
		float _yRot = Input.GetAxisRaw("Mouse X");
		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
		motor.Rotate(_rotation);

		//Rotates Player Camera Up/Down
		float _xRot = Input.GetAxisRaw("Mouse Y");
		Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;
		motor.RotateCamera(_cameraRotation);
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}
