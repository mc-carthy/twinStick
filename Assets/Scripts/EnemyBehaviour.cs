using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public int health = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log ("Hit " + collision.gameObject.name);

		if (collision.gameObject.tag == "Projectile") {
			LaserBehaviour laser = collision.gameObject.GetComponent<LaserBehaviour> ();
			health -= laser.damage;
			Destroy (collision.gameObject);
		}

		if (health <= 0) {
			Destroy (gameObject);
			GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			controller.KilledEnemy();		}
	}
}