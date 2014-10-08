using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

	//public enumerator to easily communicate current game state
	public enum GameState { PLAYING, PAUSED };
	public static GameState gameState = GameState.PLAYING;

	public float speed;
	public float turnSpeed;
	public static bool paused = false;

	private Vector3 moveDirection;

	void Start () {
	
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
		if (Input.GetKey (KeyCode.W)) 
		{
			rigidbody2D.transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			rigidbody2D.transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			rigidbody2D.transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D)) 
		{			
			rigidbody2D.transform.position += Vector3.right * speed * Time.deltaTime;
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

			UpdateGameState();
		} 
	}

//	public static enum getGameState()
//	{
//		return gameState;
//	}

	void UpdateGameState()
	{
		Time.timeScale = paused ? 0 : 1;

		gameState = paused ? GameState.PAUSED : GameState.PLAYING;
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if(other.CompareTag("goal")) 
		{
			Application.LoadLevel ("TileMapTester");
		}
	}

	public void Respawn(Vector3 spawnPt)
	{
		transform.position = spawnPt;
	}
}