using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : MonoBehaviour {

	private Transform player;
	public float speed = 2.0f;

	void Start () {
		player = FindObjectOfType<PlayerBehaviour> ().transform;
	}
	
	void Update () {
		Vector3 delta = player.position - transform.position;
		float moveSpeed = speed * Time.deltaTime;
		transform.position = transform.position + (delta * moveSpeed);
	}
}
