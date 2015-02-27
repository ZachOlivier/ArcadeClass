using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class pcScript : MonoBehaviour {

	public AudioClip deathSound;

	// Variables to keep track of time
	private float jumpTimer;
	public float jumpTime;

	// Variables to store movement and jump speeds
	public float constMoveSpeed;
	public float moveSpeed;
	public float jumpSpeed;
	public float jumpDecrease;

	// Variables to store ints for spell types
	private int spell1 = 1;
	private int spell2 = 2;
	private int spell3 = 3;

	public int health;

	// Array to hold the spell types
	public int[] spells;

	// Castable Spells
	private int[] fireball			= {1, 1, 1};
	private int[] flares			= {1, 2, 1, 2};
	private int[] piercingBlade 	= {2, 1, 1, 3};
	private int[] sunbeam			= {1, 1, 3, 3, 1};
	private int[] guillotine		= {2, 2, 3, 1, 3};
	private int[] whirlingBlade 	= {2, 3, 1, 2, 2};
	private int[] drainCube			= {3, 3, 2, 1, 3};
	private int[] necroglassWall	= {3, 2, 2, 3, 3};
	private int[] darkGrasp			= {3, 1, 2, 3, 2};

	// Variable for length of array for spells
	private int maxSpells = 5;

	// Variable for the current index # the spells array is at
	public int index = 0;

	// Variable to store how high the player can jump
	public float jumpHeight;

	// Variable to store where the ground is
	public float groundY;

	// Variable to store how fast the player falls
	public float gravity;

	// Variables to tell what state the player is currently in
	public bool isJumping = false;
	public bool isFalling = false;

	private soundScript sound;
	private manaScript mana;

	void Awake ()
	{
		// This function is mainly used in order to set variables from different gameobject's scripts
		// We do not currently need this, but we might need it later so I put it in as a placeholder
		sound = this.gameObject.GetComponentInChildren<soundScript>();
		mana = this.gameObject.GetComponent<manaScript>();
	}

	// Use this for initialization
	void Start ()
	{
		// This function is called only once right at the beginning of the game, use this to set start values
		// This is useful when you want to set something to something else, because you cant do that outside
		// of the script. Note that this function overrides the value that is set outside of the script in the
		// editor's inspector tab. This means make sure that this value is set right here, or it can mess it up

		deathSound = sound.deathSound;

		spells = new int[maxSpells];
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (health <= 0)
		{
			sound.audio.PlayOneShot(deathSound);

			health = 3;
		}

		if (mana.manaStored > 0)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				clickedSpell(spell1);
			}
			
			if (Input.GetKeyDown(KeyCode.W))
			{
				clickedSpell(spell2);
			}
			
			if (Input.GetKeyDown(KeyCode.E))
			{
				clickedSpell(spell3);
			}
		}

		else if (mana.manaStored <= 0)
		{
			Debug.Log("Out of Mana, Wait a Second - " + mana.manaStored);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			checkSpell();
			
			resetSpells();
		}

		// If the left arrow key is currently down
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			// In C# you have to set a temporary variable in order to change a gameobject's variables
			// In this case we need to make a temp variable for a position, which is Vector2 type
			// Vector2 is just a way to hold (X, Y) values. Vector3 holds (X, Y, Z) but we dont need
			// a Z because we are using 2-D. Set the temp variable to the object's current position
			Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y);

			// Subtract the X position by movement speed per second, this moves the object left in game
			position.x -= moveSpeed * Time.deltaTime;

			// Set the object's position to the temporary variable in order to get the object to move
			this.transform.position = position;
		}
	
		// Same stuff as above only this time it is for the right arrow key to move the object right
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y);

			position.x += moveSpeed * Time.deltaTime;

			this.transform.position = position;
		}

		// If the space bar key had been pressed down AND the player is NOT jumping AND NOT falling
		// We need to make sure the player is not currently in the jump/fall animation or else they
		// would be able to jump again before even finishing the first jump. This is not what we want
		if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isFalling)
		{
			// Set the player to jumping, we can not put the code below in here because this isnt a loop
			isJumping = true;
		}

		// If the player is currently in its jump animation, we put the code in the Update loop instead
		// so that this can loop correctly. We need it to loop in order to tell time and move the player
		if (isJumping)
		{
			// Increase the jumpTimer by 1 per second
			jumpTimer += Time.deltaTime;

			// Set the player's move speed to 1/4 of the usual speed because the player is in the air
			moveSpeed = constMoveSpeed / jumpDecrease;

			// Create a temporary variable just like in the code above in order to change the position
			Vector2 positionJ = new Vector2(this.transform.position.x, this.transform.position.y);

			// This time we change the Y value, adding the jump speed per second to move the player up
			positionJ.y += jumpSpeed * Time.deltaTime;

			// Set the object's position to the temporary variable in order to get the object to move
			this.transform.position = positionJ;

			// If the jumpTimer reaches the time we set for the player to be able to jump for
			if (jumpTimer >= jumpTime)
			{
				// Set the player to falling
				isFalling = true;

				// Reset the jumpTimer so that we can use the jumpTimer variable again next time they jump
				jumpTimer = 0;

				// Make sure to set the player to not jumping anymore, this must be done in order
				// to get out of this loop. This whole loop is what made the player move up. We
				// don't want the player moving up anymore after they jumped high enough. Be sure
				// to ALWAYS set the variable that takes you out of your loop LAST. Notice how the
				// main loop is "if (isJumping)". This means as soon as we set isJumping to false
				// it will take us out of the main loop, which takes us out of this loop as well.
				// Nothing else can happen after being taken out of the loop statement, be aware!
				isJumping = false;
			}
		}

		// If the player's Y position is on the ground AND the player is NOT currently jumping
		// We dont want to add the NOT falling because we want this to be checking while falling
		if (this.transform.position.y <= groundY && !isJumping && isFalling)
		{
			// Set the temporary position variable
			Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y);

			// Set the player's position to the temp variable. Notice the temp variable is set to
			// the player's current position. This means I am setting the player's position to its
			// current position. Effectively stopping the player's falling movement.
			this.transform.position = position;

			// Since we changed the move speed while the player is in the air, we need to set it back
			// Set the move speed to its default value for when the player is not jumping
			moveSpeed = constMoveSpeed;

			// Set the player to no longer falling, since the object has hit the ground. This takes us
			// out of the loop as well. Notice that since this takes us out of the loop, this is last!
			isFalling = false;
		}

		// Else if means that if the "if" statement above it is not currently looping, this statement will
		// In this case, "else" if the player is not on the ground AND is NOT currently jumping. We check
		// if the player's Y value is GREATER than the Y value of the ground, which means they are in the air
		else if (this.transform.position.y > groundY && !isJumping)
		{
			// Set the temp variable
			Vector2 positionG = new Vector2(this.transform.position.x, this.transform.position.y);

			// Decrease the Y value by gravity's speed per second, this will move the player down in game
			positionG.y -= gravity * Time.deltaTime;

			// Set the player's position to the temp variable in order to move the object down in the game
			this.transform.position = positionG;
		}
	}

	void clickedSpell (int clicked)
	{
		if (index > 4)
		{
			resetSpells();
			
			mana.manaStored--;
			
			spells[index] = clicked;
			
			Debug.Log("Full Button Pressed: #" + (index + 1) + " " + spells[index]);
			
			index++;
		}
		
		else
		{
			mana.manaStored--;
			
			spells[index] = clicked;
			Debug.Log("Button Pressed: #" + (index + 1) + " " + spells[index]);
			
			index++;
		}
	}

	void resetSpells ()
	{
		for (int i = 0; i < spells.Length; i++)
		{
			spells[i] = 0;
		}
		
		index = 0;
	}

	void checkSpell ()
	{
		if (index <= 0)
		{
			Debug.Log("Use mana to cast spells. : " + index + " mana used");
		}

		else if (index == 1 || index == 2)
		{
			Debug.Log("Not enough mana was used. : " + index + " mana used");
		}

		else if (index == 3)
		{
			for (int i = 0; i < fireball.Length; i++)
			{
				if (spells[i] == fireball[i])
				{
					if (i == fireball.Length - 1)
					{
						Debug.Log("Cast Fireball");
						CastSpell("Fireball");
					}
				}
				
				else
				{
					break;
				}
			}
		}

		else if (index == 4)
		{
			for (int i = 0; i < flares.Length; i++)
			{
				if (flares[i] == spells[i])
				{
					if (i == flares.Length - 1)
					{
						CastSpell("Flares");
						Debug.Log("Cast Flares");
					}
				}
				
				else
				{
					break;
				}
			}

			for (int i = 0; i < piercingBlade.Length; i++)
			{
				if (piercingBlade[i] == spells[i])
				{
					if (i == piercingBlade.Length - 1)
					{
						CastSpell("Piercing Blade");
						Debug.Log("Cast Piercing Blade");
					}
				}
				
				else
				{
					break;
				}
			}
		}

		else if (index == 5)
		{
			for (int i = 0; i < sunbeam.Length; i++)
			{
				if (sunbeam[i] == spells[i])
				{
					if (i == sunbeam.Length - 1)
					{
						CastSpell("Sunbeam");
						Debug.Log("Cast Sunbeam");
					}
				}
				
				else
				{
					break;
				}
			}

			for (int i = 0; i < guillotine.Length; i++)
			{
				if (guillotine[i] == spells[i])
				{
					if (i == guillotine.Length - 1)
					{
						CastSpell("Guillotine");
						Debug.Log("Cast Guillotine");
					}
				}
				
				else
				{
					break;
				}
			}

			for (int i = 0; i < whirlingBlade.Length; i++)
			{
				if (whirlingBlade[i] == spells[i])
				{
					if (i == whirlingBlade.Length - 1)
					{
						CastSpell("Whirling Blade");
						Debug.Log("Cast Whirling Blade");
					}
				}
				
				else
				{
					break;
				}
			}

			for (int i = 0; i < drainCube.Length; i++)
			{
				if (drainCube[i] == spells[i])
				{
					if (i == drainCube.Length - 1)
					{
						CastSpell("Drain Cube");
						Debug.Log("Cast Drain Cube");
					}
				}
				
				else
				{
					break;
				}
			}

			for (int i = 0; i < necroglassWall.Length; i++)
			{
				if (necroglassWall[i] == spells[i])
				{
					if (i == necroglassWall.Length - 1)
					{
						CastSpell("Necroglass Wall");
						Debug.Log("Cast Necroglass Wall");
					}
				}
				
				else
				{
					break;
				}
			}

			for (int i = 0; i < darkGrasp.Length; i++)
			{
				if (darkGrasp[i] == spells[i])
				{
					if (i == darkGrasp.Length - 1)
					{
						CastSpell("Dark Grasp");
						Debug.Log("Cast Dark Grasp");
					}
				}
				
				else
				{
					break;
				}
			}
		}

		else if (index > 5)
		{
			Debug.Log("Error casting spell. Index above spell limit. : " + index);
		}
	}

	void CastSpell (string spellName)
	{
		if (spellName == "Fireball")
		{
			// Instantiate Fireball gameobject at designated location
		}

		if (spellName == "Flares")
		{

		}

		if (spellName == "Sunbeam")
		{
			
		}

		if (spellName == "Piercing Blade")
		{
			
		}

		if (spellName == "Guillotine")
		{
			
		}

		if (spellName == "Whirling Blade")
		{
			
		}

		if (spellName == "Drain Cube")
		{
			
		}

		if (spellName == "Necroglass Wall")
		{
			
		}

		if (spellName == "Dark Grasp")
		{
			
		}
	}

	void OnGUI ()
	{
		// This function allows you to create GUI using Unity's pre-created GUI options, very useful
		// We do not currently need this, but we might need it later so I put it in as a placeholder
	}
}