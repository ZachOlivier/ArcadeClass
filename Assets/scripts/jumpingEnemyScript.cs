using UnityEngine;
using System.Collections;

public class jumpingEnemyScript : MonoBehaviour
{

	public AudioClip gameOver;
	public int health;
	public int moveSpeed;
	public float gravity;
	public float groundY;
	public float timer;
	public float nextJumpTimer;
	public float jumpTime;
	public float jumpSpeed;
	public bool jumping;
	public bool falling;

	//public GameObject projectile;

	public GameObject holder;
	public GameObject pc;
	private soundScript sound;
	private pcScript player;

	void Awake ()
	{
		sound = holder.GetComponent<soundScript> ();
		player = pc.GetComponent<pcScript> ();
	}

	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		nextJumpTimer += Time.deltaTime;
		if (nextJumpTimer >= 2) {
			// Set the player to jumping, we can not put the code below in here because this isnt a loop
			jumping = true;
			nextJumpTimer = 0;
		}
		if (jumping) {
			Vector3 position = this.transform.position;

			position.x -= moveSpeed * Time.deltaTime;

			this.transform.position = position;

			// Increase the timer by 1 per second
			timer += Time.deltaTime;

			// Create a temporary variable just like in the code above in order to change the position
			Vector2 positionJ = new Vector2 (this.transform.position.x, this.transform.position.y);

			// This time we change the Y value, adding the jump speed per second to move the player up
			positionJ.y += jumpSpeed * Time.deltaTime;

			// Set the object's position to the temporary variable in order to get the object to move
			this.transform.position = positionJ;

			// If the timer reaches the time we set for the player to be able to jump for
			if (timer >= jumpTime) {
				// Set the player to falling
				falling = true;

				// Reset the timer so that we can use the timer variable again next time they jump
				timer = 0;
				jumping = false;
			}
		}

		if (this.transform.position.y <= groundY && !jumping && falling) {
			// Set the temporary position variable
			Vector2 position = new Vector2 (this.transform.position.x, this.transform.position.y);

			// Set the player's position to the temp variable. Notice the temp variable is set to
			// the player's current position. This means I am setting the player's position to its
			// current position. Effectively stopping the player's falling movement.
			this.transform.position = position;


			// Set the player to no longer falling, since the object has hit the ground. This takes us
			// out of the loop as well. Notice that since this takes us out of the loop, this is last!
			falling = false;
		} 
		else if (this.transform.position.y > groundY && !jumping) {
			Vector3 position = this.transform.position;
			
			position.x -= moveSpeed * Time.deltaTime;
			
			this.transform.position = position;

			// Set the temp variable
			Vector2 positionG = new Vector2 (this.transform.position.x, this.transform.position.y);

			// Decrease the Y value by gravity's speed per second, this will move the player down in game
			positionG.y -= gravity * Time.deltaTime;

			// Set the player's position to the temp variable in order to move the object down in the game
			this.transform.position = positionG;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		sound.audio.PlayOneShot (gameOver);

		player.health -= 1;

		Destroy (this.gameObject);
	}
}