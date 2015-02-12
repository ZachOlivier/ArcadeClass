using UnityEngine;
using System.Collections;

public class e_shieldScript : MonoBehaviour {
	
	public AudioClip gameOver;
	
	public int health;
	public int score;
	public int damage;
	
	public int moveSpeed;
	
	public float timer;
	
	//public GameObject projectile;
	
	public GameObject holder;
	public GameObject pc;
	
	private soundScript sound;
	private pcScript player;
	private uiScript ui;
	
	void Awake ()
	{
		sound = holder.GetComponent<soundScript>();
		player = pc.GetComponent<pcScript>();
		ui = Camera.main.GetComponent<uiScript>();
	}
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.gameObject.name == "EnemyShielded(Clone)")
		{
			Vector3 position = this.transform.position;
			
			position.x -= moveSpeed * Time.deltaTime;
			
			this.transform.position = position;
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		sound.audio.PlayOneShot(gameOver);
		
		player.health -= damage;
		ui.totalScore += score;
		
		Destroy(this.gameObject);
	}
}