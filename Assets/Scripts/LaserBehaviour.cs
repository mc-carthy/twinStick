using UnityEngine;
using System.Collections;

public class LaserBehaviour : MonoBehaviour {

	public float lifetime = 2.0f;
	public float speed = 5.0f;
	public int damage = 1;

	void Start () {
		// Destroy the object once the lifetime is up
		Destroy(gameObject, lifetime);
	}
	
	void Update () {
		// Continue in the direction faced
		transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
