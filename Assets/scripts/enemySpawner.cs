using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {

	public float timer;
	public float spawnTime;
	public int minSpawnTime;
	public int maxSpawnTimer;

	public int enemiesSpawned;

	// Use this for initialization
	void Start () {
	
		timer = 0;
		enemiesSpawned = 1;
		spawnTime = Random.Range(minSpawnTime, maxSpawnTimer);
	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;

		if (timer >= spawnTime)
		{
			//Debug.Log("Enemy Spawned " + enemiesSpawned);

			enemiesSpawned++;

			timer = 0;
		}
	}
}