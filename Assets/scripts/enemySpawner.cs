using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {

	public float timer;
	public float spawnTime;

	public int minSpawnTime;
	public int maxSpawnTimer;

	public int enemiesSpawned;

	public Vector3 spawnPoint;

	public GameObject enemy;
	public GameObject jumpingEnemy;

	// Use this for initialization
	void Start () {
	
		timer = 0;
		enemiesSpawned = 0;
		spawnTime = Random.Range(minSpawnTime, maxSpawnTimer);
	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;

		if (timer >= spawnTime && enemiesSpawned % 2 == 0)
		{
			Instantiate (enemy, spawnPoint, Quaternion.identity);

			enemiesSpawned++;

			timer = 0;
		}
		else if (timer >= spawnTime)
		{
			Instantiate (jumpingEnemy, spawnPoint, Quaternion.identity);
			enemiesSpawned++;
			timer = 0;
		}
	}
}