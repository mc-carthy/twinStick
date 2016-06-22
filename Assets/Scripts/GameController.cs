using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform enemy;

	[Header("Wave Properties")]
	public float timeBeforeSpawning = 1.5f;
	public float timeBetweenEnemies = 0.25f;
	public float timeBeforeWaves = 2.0f;

	public int enemiesPerWave = 10;
	private int currentNumberOfEnemies = 0;

	[Header("User Interface")]
	private int score;
	private int waveNumber;
	public Text scoreText;
	public Text waveText;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnEnemies ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseScore(int increase) {
		score += increase;
		scoreText.text = "Score: " + score;
	}

	public void KilledEnemy() {
		currentNumberOfEnemies--;
		IncreaseScore (10);
	}

	// Coroutine to spawn enemies
	IEnumerator SpawnEnemies() {
		// Give the player time before starting the spawning
		yield return new WaitForSeconds(timeBeforeSpawning);

		// After timeBeforeSpawning elapses, enter this loop:
		while (true) {
			// Wait until all enemies are dead before spawning more
			if (currentNumberOfEnemies <= 0) {
				waveNumber++;
				waveText.text = "Wave: " + waveNumber;
				// Spawn the wave's enemies at random positions
				for (int i = 0; i < enemiesPerWave; i++) {
					// Ensure the enemies are spawned off-screen
					float randDistance = Random.Range(10, 25);
					// Direction is random
					Vector2 randDirection = Random.insideUnitCircle;
					// Set the enemies position
					Vector3 enemyPos = this.transform.position;
					enemyPos.x += randDirection.x * randDistance;
					enemyPos.y += randDirection.y * randDistance;

					// Spawn an enemy & increment the current enemy count
					Instantiate(enemy, enemyPos, this.transform.rotation);
					currentNumberOfEnemies++;
					yield return new WaitForSeconds (timeBetweenEnemies);
				}
			}
			// Check if a new wave is required at set intervals
			yield return new WaitForSeconds(timeBeforeWaves);
		}
	}
}
