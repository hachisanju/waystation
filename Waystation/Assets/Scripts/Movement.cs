/******************************************************************
 * Movement.cs is a simple script for controlling 2D character
 * movement on the primary inter-cardinal directions. It's designed
 * to be used with sprite-sheets, specifically referencing the 
 * first 24 sprites on the sheet, 3 sprites for each animation, 8
 * total directions. Holding shift lets the player run. Pressing
 * Z is considered an interaction.
 * 														-Thomas
 *****************************************************************/

using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float aniframerate = 0.2f; //Animation speed as a float.

	public float speed;
	public int speedmultiplier = 1;

	public int facing; //0-8, primary intercardinal directions, clockwise starting at south.

	public int increment; //This is used to increment through the animation.

	public float timer; //This timer is necessary to synch the animation with Unity time.

	/***************************************************************
	 * These int arrays serve as animations. They directly reference 
	 * individual sprites on the sprite sheet.
	 **************************************************************/
	public int []south = new int[] {0, 1, 0, 2};
	public int []southwest = new int[] {3, 4, 3, 5};
	public int []west = new int[] {6, 7, 6, 8};
	public int []northwest = new int[] {9, 10, 9, 11};
	public int []north = new int[] {12, 13, 12, 14};
	public int []northeast = new int[] {15, 16, 15, 17};
	public int []east = new int[] {18, 19, 18, 20};
	public int []southeast = new int[] {21, 22, 21, 23};
	public int []direction = new int[3];

	public bool Interact = false; //Is the player trying to interact with the environment?

	private SpriteRenderer sprite; //We gotta render this sucker.

	Sprite[] player;

	/**************************************************************
	 * START will initialize our sprite sheet and our renderer.
	 * By default we face towards the camera, and we set our
	 * increment to 1.
	 *************************************************************/

	void Start(){
		player = Resources.LoadAll<Sprite>("Assets/Butt.png"); 
		sprite = GetComponent<SpriteRenderer> ();
		facing = 0;
		int increment = 1;
		timer = aniframerate;

	}

	/************************************************************
	 * In TURN, to face a direction, we update the player's
	 * sprite index based on their facing direction.
	 ***********************************************************/
	void Turn(int num)
	{
		GetComponent<SpriteRenderer> ().sprite = player[num];
	}
		
	/**********************************************************
	 * During UPDATE, we slowly tick down time on our timer 
	 * based on the Unity time. If the player presses Z, we
	 * set interact to true. Otherwise we set it back to 
	 * false.
	 * *******************************************************/
	void Update()
	{
		timer -= Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.Z)) {
			Interact = true;
		}
		else
			Interact = false;
	}

	/*********************************************************
	 * Our FIXEDUPDATE keeps track of character movement.
	 * ******************************************************/
	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().freezeRotation = true; 	//This sprite should not rotate!
		float input = Input.GetAxis ("Vertical"); 			//Check if we're moving vertically.
		float input2 = Input.GetAxis ("Horizontal"); 		//Check if we're moving horizontally.

		if(Input.GetKey(KeyCode.LeftShift))
			speedmultiplier = 2;							//If the player presses shift, they DASH.
		else
			speedmultiplier = 1;							//Otherwise normal speed applies.

		GetComponent<Rigidbody2D>().AddForce (gameObject.transform.up * (speed) * input*speedmultiplier);			//Apply movement to the player axis based on input and speed.
		GetComponent<Rigidbody2D> ().AddForce (gameObject.transform.right * speed * input2*speedmultiplier);
		if(Input.GetKeyDown("/"))
			zSort();

		if (timer <= 0) {
			animate ();										//If our timer reaches zero, animate the player based on the framerate.
			timer +=aniframerate;
		}


	}

	/**********************************************************
	 * NEXTSPRITE grabs the next sprite in the current 
	 * animation, and sets the player sprite to it.
	 * *******************************************************/
	void NextSprite(int[] animation, int animation_size)
	{
		GetComponent<SpriteRenderer> ().sprite = player [animation [increment]];
		if (increment < animation_size)
			increment++;
		else
			increment = 0;
	}
	/**********************************************************
	 * ANIMATE checks the key that the player has pressed, and
	 * dependant on that, chooses which animation array to 
	 * prime for use in NEXTSPRITE.
	 * *******************************************************/
	void animate(){
		

		if (Input.GetKey("s") && Input.GetKey("a"))
		{
			direction = southwest;
			NextSprite (direction, 3);
		}
		else if (Input.GetKey("s") && Input.GetKey("d"))
		{
			direction = southeast;
			NextSprite (direction, 3);
		}
		else if (Input.GetKey("w") && Input.GetKey("a"))
		{
			direction = northwest;
			NextSprite (direction, 3);
		}
		else if (Input.GetKey("w") && Input.GetKey("d"))
		{
			direction = northeast;
			NextSprite (direction, 3);
		}
		else if (Input.GetKey("s"))
		{
			direction = south;
			NextSprite (direction, 3);
		}
		else if (Input.GetKey("a"))
		{
			direction = west;
			NextSprite (direction, 3);
		}
		else if (Input.GetKey("w"))
		{
			direction = north;
			NextSprite (direction, 3);
		}
		else if (Input.GetKey("d"))
		{
			direction = east;
			NextSprite (direction, 3);
		}
		else {
			NextSprite (direction, 0);
		}


	}

	//Not functional.
	void zSort(){

		string sw = sprite.sortingLayerName;
		if (sprite) {
			switch (sw) {
			case("C0"):
				sprite.sortingLayerName = "C1";
				gameObject.layer = 10;
				break;
			case("C1"):
				sprite.sortingLayerName = "C2";
				gameObject.layer = 12;
				break;
			case("C2"):
				sprite.sortingLayerName = "C3";
				gameObject.layer = 14;
				break;
			case("C3"):
				sprite.sortingLayerName = "C0";
				gameObject.layer = 8;
				break;
			}
			sprite.sortingOrder = 2;
		}
	}
		

}