using UnityEngine;
using System.Collections;

// Weapon script is attached to an empty gameobject as a child of the player prefab.
// Weapon is positioned on the wand.
public class MagicMissle : Spell<Vector3> {

	// Reference to the projectile rigidbody
	public GameObject bullet;
	// Reference to the transform this weapon is attached to
	private Transform parent;
	private int damage;
	// Use this for initialization

    public MagicMissle(GameObject pCaster) : base(pCaster)
    {
 
        name = "MagicMissle";

        parent = pCaster.GetComponent<Transform>();
        bullet = Resources.Load("Missile") as GameObject;
    }

	protected override void RefreshValues()
	{
		switch (level)
		{
		case 1:
			manaCost = 30;
			coolDown = 0.25f;
			damage = 10;
			break;
		case 2:
			manaCost = 6;
			coolDown = 0.2f;
			damage = 15;
			break;
		default:
			Debug.Log("MagicMissle level error");
			break;
		}
		
	}

    protected override void CastSpell(Vector3 target)
    {
		
    float bulletspeed = 800f;
    Vector3 toward = (target - Camera.main.WorldToScreenPoint(caster.transform.position)).normalized;
    float xval = toward.x;
    float yval = toward.y;

    Debug.Log(toward.x);
    toward = new Vector3(xval,yval,0);
    float angle = (Mathf.Atan2(yval,xval) * 180)/(Mathf.PI) - 90;
    Quaternion rotation = Quaternion.identity;
    rotation.eulerAngles = new Vector3(0,0,angle);
    GameObject mbolt = GameObject.Instantiate(bullet, caster.transform.position + 0.25f * toward, rotation) as GameObject;
    mbolt.GetComponent<DestroyBullet>().Initialize(damage + 2*casterStat.getIntelligence());
    mbolt.GetComponent<Rigidbody2D>().AddForce(toward*bulletspeed);
        /*
				// Get mouse position
				Vector3 mousepos =  target;
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
				
				float angle = Mathf.Atan2(yval, xval) * Mathf.Rad2Deg;
				Quaternion rotation = Quaternion.identity;
				rotation.eulerAngles = new Vector3(0, 0, angle);
	
				// The weapon is set to an empty gameobject at the position of the wand.
				// We have to flip the character to the direction of the mouse click temporarily so that
				// the projectile doesn't get blocked by the character's rigidbody when firing at
				// the direction opposite to the one the character is facing.
				// This may later be changed when we figure out a way to not make the two collide.
				Vector3 theScale = parent.localScale;
				if (xval < 0) {
					theScale.x = -1;
					parent.localScale = theScale;
				} else if (xval >= 0) {
					theScale.x = 1;
					parent.localScale = theScale;
				}
	
				// Create the bullet and add force to it.
				GameObject bult = GameObject.Instantiate (bullet, parent.position, rotation) as GameObject;
		bult.GetComponent<DestroyBullet>().Initialize(damage);
				bult.GetComponent<Rigidbody2D>().AddForce(toward);
                */
    }
}
/*	
	// Update is called once per physics update
	void FixedUpdate () {
		if (!parent.GetComponent<Status>().isStunned && Input.GetMouseButtonDown(0)) {
//			if(parent.GetComponent<Status>().mana > 10.0f) {
//				parent.GetComponent<Status>().mana -= 10.0f;
			
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
				
				float angle = Mathf.Atan2(yval, xval) * Mathf.Rad2Deg;
				Quaternion rotation = Quaternion.identity;
				rotation.eulerAngles = new Vector3(0, 0, angle);
	
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
				Rigidbody2D bult = Instantiate (bullet, transform.position, rotation) as Rigidbody2D;
				bult.AddForce(toward);
			//}
		}
	}
}
*/
