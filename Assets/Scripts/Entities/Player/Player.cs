using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed;
	public bool paused;
	private Vector3 moveDirection;
	public float turnSpeed;
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