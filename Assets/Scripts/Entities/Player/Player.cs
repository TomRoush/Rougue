using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

	public float speed;
	private Vector3 moveDirection;
	public float turnSpeed;
	public bool a;
	public bool d;

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
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
		//anim.SetBool("velocity", Input.GetKey (KeyCode.D));
		if (Input.GetKey (KeyCode.D)) 
		{		
			anim.SetBool("d", true);
			anim.SetBool("a", false);
			rigidbody2D.transform.position += Vector3.right * speed * Time.deltaTime;
		} 
		else if (Input.GetKey (KeyCode.W)) 
		{
			anim.SetBool("d",true);
			rigidbody2D.transform.position += Vector3.up * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.A)) 
		{
			anim.SetBool("a", true);
			anim.SetBool("d", false);
			rigidbody2D.transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.S)) 
		{
			anim.SetBool("d",true);
			rigidbody2D.transform.position += Vector3.down * speed * Time.deltaTime;
		}
		else{
			anim.SetBool("d",false);
			anim.SetBool("a", false);

		}
	}


	void OnTriggerEnter2D( Collider2D other )
	{
		if(other.CompareTag("goal")) 
		{
			Application.LoadLevel ("AnimationOnMap");
		}
	}

	public void Respawn(Vector3 spawnPt)
	{
		transform.position = spawnPt;
	}
}