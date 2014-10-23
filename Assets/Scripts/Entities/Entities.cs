using UnityEngine;
using System.Collections;

public abstract class Entities : MonoBehaviour {


	public float speed;
	public int health;

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
		Destroy (gameObject);
	}
}
