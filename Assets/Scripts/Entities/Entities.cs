using UnityEngine;
using System.Collections;

public abstract class Entities : MonoBehaviour {

	private float sqrt2 = 1.41421356237f;
	public float speed;
	public float health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeHealth(int amount)
	{
		health = health - amount;
		Debug.Log ("health left" + health);

		if (health <= 0) {
			Die ();
		}
	}

	public void Die()
	{
		//Destroy (gameObject);
	}

	public void moveUp()
	{
		if (!gameObject.GetComponent<Status>().isStunned)
		{
			rigidbody2D.transform.position += Vector3.up  * gameObject.GetComponent<Status> ().speed 
					* gameObject.GetComponent<Status> ().getSpeedx ()  * Time.deltaTime;
		}
	}

	public void moveDown()
	{
		if (!gameObject.GetComponent<Status>().isStunned)
		{
			rigidbody2D.transform.position += Vector3.down  * gameObject.GetComponent<Status> ().speed 
					* gameObject.GetComponent<Status> ().getSpeedx ()  * Time.deltaTime;
		}
	}

	public void moveLeft()
	{
		if (!gameObject.GetComponent<Status>().isStunned)
		{
			rigidbody2D.transform.position += Vector3.left  * gameObject.GetComponent<Status> ().speed 
					* gameObject.GetComponent<Status> ().getSpeedx ()  * Time.deltaTime;
		}
	}

	public void moveRight()
	{
		if (!gameObject.GetComponent<Status>().isStunned)
		{
			rigidbody2D.transform.position += Vector3.right  * gameObject.GetComponent<Status> ().speed 
					* gameObject.GetComponent<Status> ().getSpeedx ()  * Time.deltaTime;
		}
	}

	public void moveDirection(Vector3 x, Vector3 y)//move in average direction of x and y
	{
		Vector3 dir = Vector3.Lerp(x, y, 0.5f); //average two vectors
		rigidbody2D.transform.position += dir  
			* gameObject.GetComponent<Status> ().speed * sqrt2
			* gameObject.GetComponent<Status> ().getSpeedx ()  
			* Time.deltaTime;
	}

	public void moveDirection(Vector3 x)//move in direction x
	{
		rigidbody2D.transform.position += x  
			* gameObject.GetComponent<Status> ().speed 
			* gameObject.GetComponent<Status> ().getSpeedx ()  
			* Time.deltaTime;
	}
}
