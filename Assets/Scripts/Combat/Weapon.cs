using UnityEngine;
using System.Collections;

// Weapon script is attached to an empty gameobject as a child of the player prefab.
// Weapon is positioned on the wand.
public class Weapon : MonoBehaviour {

	// Reference to the projectile rigidbody
	public Rigidbody2D bullet;
	// Reference to the transform this weapon is attached to
	private Transform parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent;
	}
	
	// Update is called once per physics update
	void FixedUpdate () {
		if (!parent.GetComponent<Status>().isStunned && Input.GetMouseButtonDown(0)) {
			if(parent.GetComponent<Status>().mana > 10.0f) {
				parent.GetComponent<Status>().mana -= 10.0f;
			
				// Get mouse position
				Vector3 mousepos =  Input.mousePosition;
				// Get mouse position relative to the current camera viewport. Returns value between 0 and 1
				Vector3 vpp = Camera.main.ScreenToViewportPoint(mousepos);
	
				// Subtract 0.5 to make the values go from -0.5 to 0.5 to determine direction
				float bulletspeed = 800f;
				float xval = (vpp.x - 0.5f) ;
				float yval = (vpp.y - 0.5f) ;
	
				// Normalize vector components to turn it into direction vector
				float denom = Mathf.Sqrt(xval * xval + yval * yval);
				xval = xval / denom;
				yval = yval / denom;
	
				// Create vector in the direction of mouse click with magnitude of projectilespeed
				Vector3 toward = new Vector3(xval * bulletspeed, yval * bulletspeed, 1.9f);
	
				// The weapon is set to an empty gameobject at the position of the wand.
				// We have to flip the character to the direction of the mouse click temporarily so that
				// the projectile doesn't get blocked by the character's rigidbody when firing at
				// the direction opposite to the one the character is facing.
				// This may later be changed when we figure out a way to not make the two collide.
				Vector3 theScale = transform.localScale;
				if (xval < 0) {
					theScale.x = -1;
					parent.localScale = theScale;
				} else if (xval >= 0) {
					theScale.x = 1;
					parent.localScale = theScale;
				}
	
				// Create the bullet and add force to it.
				Rigidbody2D bult = Instantiate (bullet, transform.position, Quaternion.identity) as Rigidbody2D;
				bult.AddForce(toward);
			}
		}
	}
}
