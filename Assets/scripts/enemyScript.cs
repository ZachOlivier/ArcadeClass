using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public enum STATE {IDLE, MOVING, ATTACKING, DEATH};
	public enum BOSS {CRAWLER, INSECT, OBSERVER};

	public STATE currentSTATE;
	public BOSS currentBOSS;
	
	public AudioClip gameOver;
	
	public int health;
	public int walkerHealth;
	public int hopperHealth;
	public int shielderHealth;
	public int gasBladderHealth;
	public int bossCrawlerHealth;
	public int bossInsectHealth;
	public int bossObserverHealth;
	
	public int score;
	public int walkerScore;
	public int hopperScore;
	public int shielderScore;
	public int gasBladderScore;
	public int bossCrawlerScore;
	public int bossInsectScore;
	public int bossObserverScore;

	public int damage;
	public int walkerDamage;
	public int hopperDamage;
	public int gliderDamage;
	public int wormDamage;
	public int shielderDamage;
	public int gasBladderDamage;
	public int bossCrawlerDamage;
	public int bossCrawlerLazerDamage;
	public int bossInsectDamage;
	public int bossObserverDamage;

	public float moveSpeed;
	public float walkerMoveSpeed;
	public float shielderMoveSpeed;
	public float hopperMoveSpeed;
	public float gliderMoveSpeed;
	public float wormMoveSpeed;
	public float gasBladderMoveSpeed;
	public float bossCrawlerSpeed;
	public float bossInsectSpeed;
	public float bossObserverSpeed;

	public bool crawlerSwinging;

	public float gasBladderTimer;
	public float gasBladderRadius;

	public float crawlerAttackRadius;
	
	public float gravity;
	public float groundY;

	private float crawlerTimer;
	public float crawlerTimeBetweenSwings;
	private float crawlerLazerTimer;
	public float crawlerLazerTime;

	private int observerWhichWeapon;

	private float observerTimer;
	public float observerReloadTime;
	public float observerBeamTime;
	public float observerSpikeTime;
	public float observerProjectileTime;

	private bool observerReloading;

	public float slowedTimer;
	public float slowedTime;
	public float slowedFromSpeed;
	public bool darkGraspSlowed;

	public bool pushedBack;
	public float damping;
	private Vector2 pushBackPosition;
	public float pushBackDistance;
	private float pushBackTimer;
	public float pushBackTime;
		
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
	public GameObject Boundary;

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
		if (this.gameObject.name == "Orb")
		{
			currentSTATE = STATE.IDLE;
			currentBOSS = BOSS.CRAWLER;
		}

		else
		{
			currentSTATE = STATE.MOVING;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// START DEBUFFS CODE

		if (this.darkGraspSlowed)
		{
			this.slowedTimer += Time.deltaTime;
			
			if (this.slowedTimer >= this.slowedTime)
			{
				ResetMovement();

				this.darkGraspSlowed = false;
			}
		}

		if (this.pushedBack)
		{
			this.pushBackTimer += Time.deltaTime;

			this.transform.position = Vector3.Lerp(this.transform.position, this.pushBackPosition, Time.deltaTime * damping);

			if (this.pushBackTimer >= this.pushBackTime)
			{
				this.pushBackTimer = 0;

				this.pushedBack = false;
			}
		}

		// START ENEMIES CODE

		if (this.gameObject.name == "Walker")
		{
			if (health <= 0)
			{
				currentSTATE = STATE.DEATH;
			}

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
			if (health <= 0)
			{
				currentSTATE = STATE.DEATH;
			}

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
			if (health <= 0)
			{
				currentSTATE = STATE.DEATH;
			}

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
			if (health <= 0)
			{
				currentSTATE = STATE.DEATH;
			}

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
			if (health <= 0)
			{
				currentSTATE = STATE.DEATH;
			}

			if (currentSTATE == STATE.MOVING)
			{
				if (health >= 5)
				{
					Vector2 position = this.transform.position;
					
					position.x -= wormMoveSpeed * Time.deltaTime;
					
					this.transform.position = position;
				}

				else if (health < 5 && health >= 3)
				{
					Vector2 position = this.transform.position;
					
					position.x -= (wormMoveSpeed * 2) * Time.deltaTime;
					
					this.transform.position = position;
				}

				else if (health < 3 && health >= 1)
				{
					Vector2 position = this.transform.position;
					
					position.x -= (wormMoveSpeed * 3) * Time.deltaTime;
					
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
			if (health <= 0)
			{
				currentSTATE = STATE.DEATH;
			}

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

		// START BOSSES CODE

		else if (this.gameObject.name == "Orb")
		{
			if (currentSTATE != STATE.IDLE)
			{
				Boundary.collider2D.enabled = false;
			}

			else if (currentSTATE == STATE.IDLE)
			{
				Boundary.collider2D.enabled = true;
			}

			// START CRAWLER CODE

			if (currentBOSS == BOSS.CRAWLER)
			{
				if (this.health <= 0)
				{
					currentSTATE = STATE.DEATH;
				}

				if (currentSTATE == STATE.IDLE)
				{
					if (health != bossCrawlerHealth)
					{
						health = bossCrawlerHealth;
					}
				}
				
				else if (currentSTATE == STATE.MOVING)
				{
					crawlerLazerTimer += Time.deltaTime;

					Vector2 position = this.transform.position;
					
					position.x -= bossCrawlerSpeed * Time.deltaTime;
					
					this.transform.position = position;

					if (crawlerLazerTimer >= crawlerLazerTime)
					{
						// Fire lazer
						Debug.Log ("pew pew");

						crawlerLazerTimer = 0;
					}
					
					if (Vector2.Distance(this.transform.position, Pc.transform.position) <= crawlerAttackRadius)
					{
						crawlerSwinging = true;

						currentSTATE = STATE.ATTACKING;
					}
				}
				
				else if (currentSTATE == STATE.ATTACKING)
				{
					if (crawlerSwinging)
					{
						crawlerTimer += Time.deltaTime;

						if (crawlerTimer >= crawlerTimeBetweenSwings)
						{
							if (Vector2.Distance(this.transform.position, Pc.transform.position) > crawlerAttackRadius)
							{
								crawlerSwinging = false;

								currentSTATE = STATE.MOVING;
							}

							else
							{
								// Play sounds

								player.health -= bossCrawlerDamage;

								crawlerTimer = 0;
							}
						}
					}
				}
				
				else if (currentSTATE == STATE.DEATH)
				{
					// Play animations and sounds

					currentBOSS = BOSS.INSECT;

					currentSTATE = STATE.IDLE;
				}
			}

			// START INSECT CODE

			else if (currentBOSS == BOSS.INSECT)
			{
				if (this.health <= 0)
				{
					currentSTATE = STATE.DEATH;
				}

				if (currentSTATE == STATE.IDLE)
				{
					if (health != bossInsectHealth)
					{
						health = bossInsectHealth;
					}
				}
				
				else if (currentSTATE == STATE.MOVING)
				{
					Vector2 position = this.transform.position;
					
					position.x -= bossInsectSpeed * Time.deltaTime;
					
					this.transform.position = position;
				}
				
				else if (currentSTATE == STATE.ATTACKING)
				{
					
				}
				
				else if (currentSTATE == STATE.DEATH)
				{
					// Play animations and sounds

					currentBOSS = BOSS.OBSERVER;
					
					currentSTATE = STATE.IDLE;
				}
			}

			// START OBSERVER CODE

			else if (currentBOSS == BOSS.OBSERVER)
			{
				if (this.health <= 0)
				{
					currentSTATE = STATE.DEATH;
				}

				if (currentSTATE == STATE.IDLE)
				{
					if (health != bossObserverHealth)
					{
						health = bossObserverHealth;
					}

					if (moveSpeed != bossObserverSpeed)
					{
						moveSpeed = bossObserverSpeed;
					}
				}
				
				else if (currentSTATE == STATE.MOVING)
				{
					observerTimer += Time.deltaTime;

					Vector2 position = this.transform.position;
					
					position.x -= moveSpeed * Time.deltaTime;
					
					this.transform.position = position;

					if (observerTimer >= observerReloadTime)
					{
						if (observerReloading)
						{
							moveSpeed = 0;

							observerWhichWeapon = Random.Range(0, 3);

							if (observerWhichWeapon <= 0)
							{
								observerTimer -= observerSpikeTime;
							}
							
							else if (observerWhichWeapon == 1)
							{
								observerTimer -= observerProjectileTime;
							}
							
							else if (observerWhichWeapon >= 2)
							{
								observerTimer -= observerBeamTime;
							}

							observerReloading = false;
						}

						else
						{
							if (observerWhichWeapon <= 0)
							{
								// Do spike

								Debug.Log ("Spike");
							}
							
							else if (observerWhichWeapon == 1)
							{
								// Do Projectile

								Debug.Log ("Projectile");
							}
							
							else if (observerWhichWeapon >= 2)
							{
								// Do Beam

								Debug.Log ("Beam");
							}

							moveSpeed = bossObserverSpeed;

							observerReloading = true;
							observerTimer = 0;
						}
					}
				}
				
				else if (currentSTATE == STATE.ATTACKING)
				{
					
				}
				
				else if (currentSTATE == STATE.DEATH)
				{
					// Play animations and sounds

					Application.LoadLevel (3);
				}
			}
		}
	}

	public void SlowMovement (float timeSlowed, float movementChange)
	{
		this.slowedTime = timeSlowed;

		this.slowedFromSpeed = this.moveSpeed;

		this.moveSpeed = this.moveSpeed * movementChange;
	}

	public void ResetMovement ()
	{
		this.slowedTimer = 0;

		this.moveSpeed = this.slowedFromSpeed;
	}

	public void PushBack ()
	{
		this.pushBackPosition = this.transform.TransformPoint(this.transform.position.x - pushBackDistance, this.transform.position.y, this.transform.position.z);

		this.pushedBack = true;
	}
}