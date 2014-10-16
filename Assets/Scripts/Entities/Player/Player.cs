using UnityEngine;
using System.Collections;

public class Player : Entities 
{

	//public enumerator to easily communicate current game state
	public enum GameState { PLAYING, PAUSED, MENU };
	public static GameState playerState = GameState.PLAYING;

	//replaced with Entities global speed
	//public float speed;
	public float turnSpeed;
	public static bool paused = false;

	private Vector3 moveDirection;

	void Start () {

		paused = false;
	
	}
	
	void Update () 
	{
		////Attempted mouse implementation
		/*Vector3 currentPosition = transform.position;
		Vector3 targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//targetPosition.z = 0;
		if (Input.GetButton("Fire1"))
		{
			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();
		}
		//while(Input.GetButton ("Fire1"))
		{
			//Vector3 target = moveDirection * moveSpeed + currentPosition;
			//if(!((targetPosition.x).Equals(this.rigidbody2D.position.x) && !(targetPosition.y).Equals(rigidbody2D.position.y)))
			//transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
		}
		//*/

		//consider taking out for less calls
		if (!gameObject.GetComponent<Status> ().isStunned) {

			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
					rigidbody2D.transform.position += Vector3.up * gameObject.GetComponent<Status> ().speed 
							* gameObject.GetComponent<Status> ().getSpeedx () * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
					rigidbody2D.transform.position += Vector3.left * gameObject.GetComponent<Status> ().speed 
							* gameObject.GetComponent<Status> ().getSpeedx () * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
					rigidbody2D.transform.position += Vector3.down * gameObject.GetComponent<Status> ().speed 
							* gameObject.GetComponent<Status> ().getSpeedx () * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {			
					rigidbody2D.transform.position += Vector3.right * gameObject.GetComponent<Status> ().speed 
							* gameObject.GetComponent<Status> ().getSpeedx () * Time.deltaTime;
			}
		}
		if(Input.GetKeyDown (KeyCode.Escape)) 
		{
			if(paused)
			{
				paused = false;
			}
			else
			{
				paused = true;
			}

		}


		//if(health <= 0)
		//{
		//	Die();		
		//}
		
		


		UpdateGameState();
	}

	//NEEDS TO BE CALLED
	
	void UpdateGameState()
	{
		Time.timeScale = paused ? 0 : 1;
		
		playerState = paused ? GameState.PAUSED : GameState.PLAYING;

	}

	void OnGUI() 
	{
		if (paused) 
		{
			if(GUI.Button (new Rect((Screen.width)/2, ((Screen.height)/2)-50, 100, 50), "CONTINUE")) 
			{
				paused = false;
				//	Debug.Log("BUTTON HIT");
			}
			if(GUI.Button (new Rect((Screen.width)/2, ((Screen.height)/2)+50, 100, 50), "SAVE & QUIT")) 
			{
				paused = true;
				Application.LoadLevel("MainMenu");
			}
		}
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if(other.CompareTag("goal")) 
		{
			Application.LoadLevel ("TileMapTester");
		} else
		{
			Application.LoadLevel ("MainMenu");
		}
	}




	public void Respawn(Vector3 spawnPt)
	{
		transform.position = spawnPt;
	}

	public void Die()
	{
		print ("I've been killed");
		Debug.Break ();
	}
}