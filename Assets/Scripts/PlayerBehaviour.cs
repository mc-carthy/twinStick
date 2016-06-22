using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {

	public float playerSpeed = 4.0f;
	public Transform laser;
	// How far from the centre of the ship the laser will be
	public float laserDistance = 0.2f;
	// Time (in seconds) between shots fired
	public float timeBetweenFires = 0.3f;
	// The buttons we can use to fire shots
	public List<KeyCode> shootButton;
	public AudioClip shootSound;


	private float currentSpeed = 0.0f;
	private float timeToNextFire = 0.0f;
	private Vector3 lastMovement = new Vector3();
	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
	}

	void Update() {
		Rotate();
		Movement();

		foreach (KeyCode button in shootButton) {
			if (Input.GetKey (button) && timeToNextFire < 0) {
				timeToNextFire = timeBetweenFires;
				ShootLaser ();
				break;
			}
		}
		timeToNextFire -= Time.deltaTime;
	}

	// Rotate the transform to face the cursor
	void Rotate () {
		Vector3 worldPos = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint (worldPos);

		float dx = this.transform.position.x - worldPos.x;
		float dy = this.transform.position.y - worldPos.y;

		// Get angle between cursor and player
		float angle = Mathf.Atan2 (dy, dx) * Mathf.Rad2Deg;

		// Convert the above angle into a vector
		Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));

		this.transform.rotation = rot;
	}

	// Translate the ship using keyboard input
	void Movement() {
		Vector3 movement = new Vector3 ();

		// Check for input
		//movement.x += Input.GetAxis ("Horizontal");
		movement.x += Input.GetAxisRaw ("Horizontal");
		//movement.y += Input.GetAxis ("Vertical");
		movement.y += Input.GetAxisRaw ("Vertical");

		movement.Normalize ();

		// Check if a button has been pressed
		if (movement.magnitude > 0) {
			// If so, move in that direction
			currentSpeed = playerSpeed;
			this.transform.Translate (movement * Time.deltaTime * playerSpeed, Space.World);
			lastMovement = movement;
		} else {
			// If not, keep moving in the direction we were previously going
			this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);	
			// Slow down over time
			currentSpeed *= 0.9f;
		}
	}

	// Create a laser and position it in front of the ship
	void ShootLaser() {
		// Get the ships position
		Vector3 laserPos = this.transform.position;
		// The angle the laser will move away from the center
		float rotationAngle = transform.localEulerAngles.z - 90;
		// Calculate the point in front of the ship at a distance of laserDistance
		laserPos.x += (Mathf.Cos ((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
		laserPos.y += (Mathf.Sin ((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);

		// Create the laser at the given point
		Instantiate(laser, laserPos, this.transform.rotation);
		audioSource.PlayOneShot (shootSound);
	}
}
