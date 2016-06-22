using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public float playerSpeed = 4.0f;

	private float currentSpeed = 0.0f;

	private Vector3 lastMovement = new Vector3();

	void Update() {
		Rotate();
		Movement();
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
}
