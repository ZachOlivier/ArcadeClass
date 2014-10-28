using UnityEngine;
using System.Collections;

public class pcScript : MonoBehaviour {

	// Variables to keep track of time
	public float timer;
	public float time;
	public float jumpTime;

	// Variables to store movement and jump speeds
	public float constMoveSpeed;
	public float moveSpeed;
	public float jumpSpeed;

	// Variable to store how high the player can jump
	public float jumpHeight;

	// Variable to store where the ground is
	public float groundY;

	// Variable to store how fast the player falls
	public float gravity;

	// Variables to tell what state the player is currently in
	public bool isJumping = false;
	public bool isFalling = false;

	void Awake ()
	{
		// This function is mainly used in order to set variables from different gameobject's scripts
		// We do not currently need this, but we might need it later so I put it in as a placeholder
	}

	// Use this for initialization
	void Start ()
	{
		// This function is called only once right at the beginning of the game, use this to set start values
		// This is useful when you want to set something to something else, because you cant do that outside
		// of the script. Note that this function overrides the value that is set outside of the script in the
		// editor's inspector tab. This means make sure that this value is set right here, or it can mess it up
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the left arrow key is currently down
		if (Input.GetKey(KeyCode.LeftArrow))
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
		if (Input.GetKey(KeyCode.RightArrow))
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
			// Increase the timer by 1 per second
			timer += Time.deltaTime;

			// Set the player's move speed to 1/4 of the usual speed because the player is in the air
			moveSpeed = constMoveSpeed / 4;

			// Create a temporary variable just like in the code above in order to change the position
			Vector2 positionJ = new Vector2(this.transform.position.x, this.transform.position.y);

			// This time we change the Y value, adding the jump speed per second to move the player up
			positionJ.y += jumpSpeed * Time.deltaTime;

			// Set the object's position to the temporary variable in order to get the object to move
			this.transform.position = positionJ;

			// If the timer reaches the time we set for the player to be able to jump for
			if (timer >= jumpTime)
			{
				// Set the player to falling
				isFalling = true;

				// Reset the timer so that we can use the timer variable again next time they jump
				timer = 0;

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

	void OnGUI ()
	{
		// This function allows you to create GUI using Unity's pre-created GUI options, very useful
		// We do not currently need this, but we might need it later so I put it in as a placeholder
	}
}