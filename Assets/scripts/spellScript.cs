using UnityEngine;
using System.Collections;

public class spellScript : MonoBehaviour {

	private bool skewerCanIncrease = true;
	public float skewerMoveSpeed;
	public float skewerSpeedIncrease;
	public float skewerChangeDistance;

	public float arcboltMoveSpeed;
	public float arcboltYSpeed;
	public float arcboltBleedOff;
	public float arcboltBoltSpeed;
	private int arcboltBoltAmount = 3;
	public float arcboltBleedOffSpeed;
	private float arcboltTimer;
	public float arcboltShootTime;
	public float arcboltShootCD;
	
	public float darkGraspDistanceAway;
	private float darkGraspClosestDistance;
	private GameObject darkGraspClosestEnemy;
	private float darkGraspTimer;
	private float darkGraspTimeSlow;
	public float darkGraspSlowAmount;
	public float darkGraspHitDistance;
	public float darkGraspMoveSpeed;

	public float drainCubeMoveSpeed;

	public float fireballMoveSpeed;

	private float necroglassWallTimer;
	public float necroglassWallTime;

	public float piercingBladeMoveSpeed;

	private float whirlingBladeTimer;
	public float whirlingBladeTime;

	private GameObject[] enemyArray;

	public float moveSpeed;

	public Transform pc;

	private pcScript player;
	private enemyScript enemy;
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<pcScript>();
	}

	// Use this for initialization
	void Start ()
	{
		// START SPELLS CODE

		if (this.gameObject.name == "Dark Grasp")
		{
			enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

			for (int i = 0; i < enemyArray.Length; i++)
			{
				darkGraspDistanceAway = Vector2.Distance(this.gameObject.transform.position, enemyArray[i].transform.position);

				if (darkGraspDistanceAway < darkGraspClosestDistance)
				{
					darkGraspClosestDistance = darkGraspDistanceAway;

					darkGraspClosestEnemy = enemyArray[i];
				}
			}

			enemy = darkGraspClosestEnemy.GetComponent<enemyScript>();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// START SPELLS CODE

		if (this.gameObject.name == "Acceleration Skewer")
		{
			if (!skewerCanIncrease)
			{
				skewerMoveSpeed = -skewerMoveSpeed;
			}

			if (Vector2.Distance(this.transform.position, pc.transform.position) < skewerChangeDistance)
			{
				Vector2 position = this.transform.position;
				
				position.x += skewerMoveSpeed * Time.deltaTime;
				
				this.transform.position = position;
			}

			else if (Vector2.Distance(this.transform.position, pc.transform.position) >= skewerChangeDistance)
			{
				skewerCanIncrease = false;
			}
		}

		if (this.gameObject.name == "Arcbolt")
		{
			Vector2 position = this.transform.position;

			position.x += arcboltMoveSpeed * Time.deltaTime;
			position.y += (arcboltYSpeed + arcboltBleedOff) * Time.deltaTime;
				
			this.transform.position = position;
				
			arcboltBleedOff -= arcboltBleedOffSpeed;

			arcboltTimer += Time.deltaTime;

			if (arcboltTimer >= arcboltShootTime)
			{
				// Instantiate an arcbolt bolt @ bottom of arcbolt

				arcboltShootTime += arcboltShootCD;
			}
		}

		if (this.gameObject.name == "Arcbolt Bolt")
		{
			Vector2 position = this.transform.position;
			
			position.y -= arcboltBoltSpeed * Time.deltaTime;
			
			this.transform.position = position;
		}
		
		if (this.gameObject.name == "Dark Grasp")
		{
			if (Vector2.Distance(this.transform.position, darkGraspClosestEnemy.transform.position) <= darkGraspHitDistance)
			{
				darkGraspTimer += Time.deltaTime;

				if (enemy.darkGraspSlowed == false)
				{
					enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

					enemy.SlowMovement(darkGraspTimeSlow, 0);

					enemy.darkGraspSlowed = true;
				}

				for (int i = 0; i < enemyArray.Length; i++)
				{
					darkGraspDistanceAway = Vector2.Distance(this.gameObject.transform.position, enemyArray[i].transform.position);

					if (darkGraspDistanceAway < darkGraspClosestDistance)
					{
						enemy = enemyArray[i].GetComponent<enemyScript>();

						if (enemy.darkGraspSlowed == false)
						{
							enemy.SlowMovement(darkGraspTimeSlow, darkGraspSlowAmount);

							enemy.darkGraspSlowed = true;
						}
					}
				}

				if (darkGraspTimer >= darkGraspTimeSlow)
				{
					for (int i = 0; i < enemyArray.Length; i++)
					{
						enemy = enemyArray[i].GetComponent<enemyScript>();

						if (enemy.darkGraspSlowed == true)
						{
							enemy.ResetMovement();
						}
					}

					Destroy(this.gameObject);
				}
			}

			else
			{
				Vector2 position = this.transform.position;
				
				position.x += darkGraspMoveSpeed * Time.deltaTime;
				
				this.transform.position = position;
			}
		}

		if (this.gameObject.name == "Drain Cube")
		{
			Vector2 position = this.transform.position;
			
			position.x += drainCubeMoveSpeed * Time.deltaTime;
			
			this.transform.position = position;
		}

		if (this.gameObject.name == "Essence Zone")
		{
			
		}

		if (this.gameObject.name == "Fireball")
		{
			Vector2 position = this.transform.position;
			
			position.x += fireballMoveSpeed * Time.deltaTime;
			
			this.transform.position = position;
		}

		if (this.gameObject.name == "Flares")
		{

		}

		if (this.gameObject.name == "Gravity Swell")
		{
			
		}

		if (this.gameObject.name == "Guillotine")
		{
			
		}

		if (this.gameObject.name == "Mental Break")
		{
			Vector2 position = this.transform.position;
			
			position.x += fireballMoveSpeed * Time.deltaTime;
			
			this.transform.position = position;
		}

		if (this.gameObject.name == "Mind Barrier")
		{
			
		}

		if (this.gameObject.name == "Necroglass Wall")
		{
			necroglassWallTimer += Time.deltaTime;

			if (necroglassWallTimer >= necroglassWallTime)
			{
				Destroy(this.gameObject);
			}
		}

		if (this.gameObject.name == "Overload")
		{
			
		}

		if (this.gameObject.name == "Piercing Blade")
		{
			Vector2 position = this.transform.position;
			
			position.x += piercingBladeMoveSpeed * Time.deltaTime;
			
			this.transform.position = position;
		}

		if (this.gameObject.name == "Sunbeam")
		{
			
		}

		if (this.gameObject.name == "Unleash")
		{
			
		}

		if (this.gameObject.name == "Whirling Blade")
		{
			whirlingBladeTimer += Time.deltaTime;

			if (whirlingBladeTimer >= whirlingBladeTime)
			{
				Destroy(this.gameObject);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (this.gameObject.name == "Acceleration Skewer")
		{
			if (other.gameObject.tag == "Enemy")
			{
				if (skewerCanIncrease)
				{
					skewerMoveSpeed += skewerSpeedIncrease;
				}
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				skewerCanIncrease = false;
			}
		}

		if (this.gameObject.name == "Arcbolt")
		{
			if (other.gameObject.tag == "Enemy")
			{

			}

			else if (other.gameObject.tag == "Shield")
			{

			}
		}

		if (this.gameObject.name == "Arcbolt Bolt")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Drain Cube")
		{
			// GONNA hurt the enemy, heal the player
			// OR supposed to put a debuff on enemy in which when enemy dies it heals the player

			if (other.gameObject.tag == "Enemy")
			{
				Destroy(this.gameObject);
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				Destroy(this.gameObject);
			}
		}
		
		if (this.gameObject.name == "Essence Zone")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Fireball")
		{
			if (other.gameObject.tag == "Enemy")
			{
				Destroy(this.gameObject);
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				Destroy(this.gameObject);
			}
		}
		
		if (this.gameObject.name == "Flares")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Gravity Swell")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Guillotine")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Mental Break")
		{
			// Push player backwards and deal damage

			if (other.gameObject.tag == "Enemy")
			{
				Destroy(this.gameObject);
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				Destroy(this.gameObject);
			}
		}
		
		if (this.gameObject.name == "Mind Barrier")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Overload")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Piercing Blade")
		{
			// Deal damage, pierces through shield

			if (other.gameObject.tag == "Enemy")
			{
				Destroy(this.gameObject);
			}
		}

		if (this.gameObject.name == "Sunbeam")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Unleash")
		{
			if (other.gameObject.tag == "Enemy")
			{
				
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				
			}
		}
		
		if (this.gameObject.name == "Whirling Blade")
		{
			if (other.gameObject.tag == "Enemy")
			{
				// Damage enemy
			}
			
			else if (other.gameObject.tag == "Shield")
			{
				Destroy(this.gameObject);
			}
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (this.gameObject.name == "Whirling Blade")
		{
			if (col.gameObject.tag == "Enemy")
			{
				// Damage enemy
			}
			
			else if (col.gameObject.tag == "Shield")
			{
				Destroy(this.gameObject);
			}
		}
	}
}