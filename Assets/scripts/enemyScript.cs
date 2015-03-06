using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public enum STATE {IDLE, MOVING, ATTACKING, DEATH};

	public STATE currentSTATE;
	
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
	public int gliderDamage;
	public int wormDamage;
	public int shielderDamage;
	public int gasBladderDamage;

	public int moveSpeed;
	public int walkerMoveSpeed;
	public int shielderMoveSpeed;
	public int hopperMoveSpeed;
	public int gliderMoveSpeed;
	public int wormMoveSpeed;
	public int gasBladderMoveSpeed;

	public float gasBladderTimer;
	public float gasBladderRadius;
	
	public float gravity;
	public float groundY;
	
	public float timer;
	public float nextJumpTimer;
	public float jumpTime;
	public float jumpBleedOff;
	
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
				Vector2 position = this.transform.position;
				if (nextJumpTimer >= timer)
				{
					position.x -= hopperMoveSpeed * Time.deltaTime;

					position.y += (jumpSpeed + jumpBleedOff) * Time.deltaTime;
					
					this.transform.position = position;

					jumpBleedOff -= .05f;

					jumping = true;
				}
				if (jumping && position.y <= groundY)
				{
					jumping = false;
					jumpBleedOff = 1;
					nextJumpTimer = 0;
					this.transform.position = new Vector2(position.x, 0);
				}
			}

			else if (currentSTATE == STATE.ATTACKING)
			{
				
			}
			
			else if (currentSTATE == STATE.DEATH)
			{
				
			}
		}

		else if (this.gameObject.name == "Glider")
		{
			if (currentSTATE == STATE.MOVING)
			{
				Vector2 position = this.transform.position;
				
				position.x -= gliderMoveSpeed * Time.deltaTime;
				
				this.transform.position = position;
			}
			
			else if (currentSTATE == STATE.ATTACKING)
			{
				
			}
			
			else if (currentSTATE == STATE.DEATH)
			{
				
			}
		}

		else if (this.gameObject.name == "DarkWorm")
		{
			if (currentSTATE == STATE.MOVING)
			{
				if (health >= 5)
				{
					Vector2 position = this.transform.position;
					
					position.x -= wormMoveSpeed * Time.deltaTime;
					
					this.transform.position = position;
				}
				else if (health >= 3)
				{
					Vector2 position = this.transform.position;
					
					position.x -= (wormMoveSpeed + 1) * Time.deltaTime;
					
					this.transform.position = position;
				}
				else if (health >= 1)
				{
					Vector2 position = this.transform.position;
					
					position.x -= (wormMoveSpeed = 3) * Time.deltaTime;
					
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