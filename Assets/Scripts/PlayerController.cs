using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/Player Controller")]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
	[SerializeField]
	public float speed = 5f;
	[SerializeField]
	public float lookSensitivity = 1f;
	[SerializeField]
	public float jumpSpeed = 8f;
	[SerializeField]
	public float gravity = 20f;

	private Vector3 _velocity = Vector3.zero;
	private PlayerMotor motor;

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion originalRotation;

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
			_velocity *= speed;
			if (Input.GetButtonDown("Jump")) {
				_velocity.y = jumpSpeed;
			}
		}
		else {
			_velocity = new Vector3(0, _velocity.y, _zMov);
			_velocity = transform.TransformDirection(_velocity);
			_velocity.x *= speed;
			_velocity.z *= speed;
		}

		_velocity.y -= gravity * Time.deltaTime;
		motor.Move(_velocity);
	}

	private void HandleRotation() {
		// //Rotates Player Left/Right
		// float _yRot = Input.GetAxisRaw("Mouse X");
		// Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
		// motor.Rotate(_rotation);

		// //Rotates Player Camera Up/Down
		// float _xRot = Input.GetAxisRaw("Mouse Y");
		// Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;
		// motor.RotateCamera(_cameraRotation);

		if (axes == RotationAxes.MouseXAndY)
		{
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			motor.Rotate(originalRotation * xQuaternion);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			motor.Rotate(originalRotation * yQuaternion);
		}
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
