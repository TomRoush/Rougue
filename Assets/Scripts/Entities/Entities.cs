using UnityEngine;
using System.Collections;

public abstract class Entities : MonoBehaviour {


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
	}

	public void moveRight()
	{
		if (!gameObject.GetComponent<Status>().isStunned)
		{
		rigidbody2D.transform.position += Vector3.right  * speed * Time.deltaTime;
		}
	}
}
