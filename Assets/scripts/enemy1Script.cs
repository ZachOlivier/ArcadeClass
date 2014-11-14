using UnityEngine;
using System.Collections;

public class enemy1Script : MonoBehaviour {

	public AudioClip gameOver;

	public int health;

	public int moveSpeed;

	public float timer;

	//public GameObject projectile;

	public GameObject holder;
	public GameObject pc;

	private soundScript sound;
	private pcScript player;

	void Awake ()
	{
		sound = holder.GetComponent<soundScript>();
		player = pc.GetComponent<pcScript>();
	}

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 position = this.transform.position;

		position.x -= moveSpeed * Time.deltaTime;

		this.transform.position = position;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		sound.audio.PlayOneShot(gameOver);

		player.health -= 1;

		Destroy(this.gameObject);
	}
}