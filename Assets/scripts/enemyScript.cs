using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	private enum STATE {IDLE, MOVING, ATTACKING, DEATH};

	private STATE currentSTATE;
	
	public AudioClip gameOver;
	
	public int health;
	public int walkerHealth;
	public int hopperHealth;
	public int shielderHealth;
	public int gasBladderHealth;

	public int score;
	public int walkerScore;
	public int hopperScore;
	public int shielderScore;
	public int gasBladderScore;

	public int damage;
	public int walkerDamage;
	public int hopperDamage;
	public int shielderDamage;
	public int gasBladderDamage;

	public int moveSpeed;
	public int walkerMoveSpeed;
	public int shielderMoveSpeed;
	public int hopperMoveSpeed;
	public int gasBladderMoveSpeed;

	public float gasBladderTimer;
	public float gasBladderRadius;
	
	public float gravity;
	public float groundY;
	
	public float timer;
	public float nextJumpTimer;
	public float jumpTime;
	
	public float jumpSpeed;
	
	private bool jumping;
	private bool falling;
	
	public GameObject Holder;
	public GameObject Pc;
	public GameObject Grandma;

	public GameObject[] NearbyObjects;
	
	private soundScript sound;
	private pcScript player;
	private cameraScript cam;
	private enemyScript enemy;
	private grandmaScript grandma;

	void Awake ()
	{
		player = Pc.GetComponent<pcScript>();
		grandma = Grandma.GetComponent<grandmaScript>();
	}

	// Use this for initialization
	void Start ()
	{
		currentSTATE = STATE.MOVING;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (health <= 0)
		{
			currentSTATE = STATE.DEATH;
		}

		if (this.gameObject.name == "Walker")
		{
			if (currentSTATE == STATE.MOVING)
			{
				Vector2 position = this.transform.position;
				
				position.x -= walkerMoveSpeed * Time.deltaTime;
				
				this.transform.position = position;
			}

			else if (currentSTATE == STATE.ATTACKING)
			{

			}

			else if (currentSTATE == STATE.DEATH)
			{

			}
		}

		else if (this.gameObject.name == "Shielder")
		{
			if (currentSTATE == STATE.MOVING)
			{
				Vector2 position = this.transform.position;
				
				position.x -= walkerMoveSpeed * Time.deltaTime;
				
				this.transform.position = position;
			}

			else if (currentSTATE == STATE.ATTACKING)
			{
				
			}
			
			else if (currentSTATE == STATE.DEATH)
			{
				
			}
		}

		else if (this.gameObject.name == "Hopper")
		{
			if (currentSTATE == STATE.MOVING)
			{
				nextJumpTimer += Time.deltaTime;
				
				if (nextJumpTimer >= 2 && !jumping && !falling) {
					// Set the player to jumping, we can not put the code below in here because this isnt a loop
					jumping = true;
					nextJumpTimer = 0;
				}
				
				if (jumping) {

					// Increase the timer by 1 per second
					timer += Time.deltaTime;

					Vector2 position = this.transform.position;
					
					position.x -= hopperMoveSpeed * Time.deltaTime;

					// This time we change the Y value, adding the jump speed per second to move the player up
					position.y += jumpSpeed * Time.deltaTime;

					// Set the object's position to the temporary variable in order to get the object to move
					this.transform.position = position;
					
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
					Vector2 position = this.transform.position;
					
					// Set the player's position to the temp variable. Notice the temp variable is set to
					// the player's current position. This means I am setting the player's position to its
					// current position. Effectively stopping the player's falling movement.
					this.transform.position = position;
					
					
					// Set the player to no longer falling, since the object has hit the ground. This takes us
					// out of the loop as well. Notice that since this takes us out of the loop, this is last!
					falling = false;
				}
				
				else if (this.transform.position.y > groundY && !jumping) {
					Vector2 position = this.transform.position;
					
					position.x -= hopperMoveSpeed * Time.deltaTime;

					// Decrease the Y value by gravity's speed per second, this will move the player down in game
					position.y -= gravity * Time.deltaTime;
					
					this.transform.position = position;
				}
			}

			else if (currentSTATE == STATE.ATTACKING)
			{
				
			}
			
			else if (currentSTATE == STATE.DEATH)
			{
				
			}
		}

		else if (this.gameObject.name == "GasBladder")
		{
			if (currentSTATE == STATE.MOVING)
			{
				Vector2 position = this.transform.position;
				
				position.x -= gasBladderMoveSpeed * Time.deltaTime;
				
				this.transform.position = position;

				if (Vector2.Distance(this.transform.position, Pc.transform.position) < gasBladderRadius)
				{
					currentSTATE = STATE.DEATH;
				}
			}

			else if (currentSTATE == STATE.DEATH)
			{
				NearbyObjects = GameObject.FindGameObjectsWithTag("Enemy");

				for (int i = 0; i < NearbyObjects.Length; i++)
				{
					enemy = NearbyObjects[i].GetComponent<enemyScript>();

					if (Vector2.Distance(this.transform.position, NearbyObjects[i].transform.position) < gasBladderRadius)
					{
						enemy.health -= gasBladderDamage;

						Destroy(NearbyObjects[i].gameObject);
					}
				}

				if (Vector2.Distance(this.transform.position, Pc.transform.position) < gasBladderRadius)
				{
					player.health -= gasBladderDamage;
				}

				// Don't need this because it is an Enemy tag already. It will die in the for loop.
				//Destroy(this.gameObject);
			}
		}
	}
}