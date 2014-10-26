using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Rigidbody2D bullet;
	private Transform parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetMouseButtonDown(0)) {
			if(parent.GetComponent<Status>().mana > 10.0f) {
				parent.GetComponent<Status>().mana -= 10.0f;
			
				Vector3 mousepos =  Input.mousePosition;
				Vector3 vpp = Camera.main.ScreenToViewportPoint(mousepos);
	
				float mult = 800f;
				float xval = (vpp.x - 0.5f) ;
				float yval = (vpp.y - 0.5f) ;
	
				float denom = Mathf.Sqrt(xval * xval + yval * yval);
				xval = xval / denom;
				yval = yval / denom;
	
				Vector3 toward = new Vector3(xval * mult, yval * mult, 1.9f);
	
				Vector3 theScale = transform.localScale;
				if (xval < 0) {
					theScale.x = -1;
					parent.localScale = theScale;
				} else if (xval >= 0) {
					theScale.x = 1;
					parent.localScale = theScale;
				}
	
				Rigidbody2D bult = Instantiate (bullet, transform.position, Quaternion.identity) as Rigidbody2D;
				bult.AddForce(toward);
			}
		}
	}
}
