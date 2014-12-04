using UnityEngine;
using System.Collections;

public class AstralProjection : Spell<GameObject> {
	
	//public float mudballTimer;
	public GameObject projection;
	public GameObject closest;
	public GameObject player;
	
	// Use this for initialization
	public AstralProjection (GameObject pCaster) : base(pCaster) {
		name = "AstralProjection";
		projection = Resources.Load ("AstralProjection") as GameObject;
		cooldownIcon = Resources.Load("Artwork/InGame/Fortify") as Texture2D;
		//player = GameObject.FindGameObjectWithTag ("Player");
		//mudballTimer = 0; 
		//parent = transform.parent;
		//fireball = (GameObject) Resources.Load ("Fireball");
		
	}
	
	protected override void RefreshValues()
	{
		switch (level)
		{
		case 1:
			manaCost = 40;
			coolDown = 7;
			break;
		case 2:
			manaCost = 40;
			coolDown = 7;
			break;
		default:
			Debug.Log("Mudball level error");
			break;
		}
		
	}
	// Update is called once per frame
	protected override void CastSpell (GameObject closest) {
		player = caster;
		Debug.Log ("333");
		if (!player.GetComponent<Status>().isStunned && closest != null) {
			//closest = player.GetComponent<Status>().FindClosestEnemy();
			//Debug.Log("mud");
			
			//Vector3 vpp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			Debug.Log("234");
			float bulletspeed = 700f;
			//float xval = (closest.transform.position.x - player.transform.position.x) ;
			//float yval = (closest.transform.position.y - player.transform.position.y) ;
			float xval = (closest.transform.position.x - player.transform.position.x) ;
			float yval = (closest.transform.position.y - player.transform.position.y) ;
			Vector3 toward = new Vector3(xval * bulletspeed, yval * bulletspeed, 1.9f)/caster.GetComponent<Status>().getDistance(closest);
			
			float angle = Mathf.Atan2(yval, xval) * 180 / (Mathf.PI) - 90;
			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(0, 0, angle);
			GameObject aprojection = GameObject.Instantiate (projection, player.transform.position + 100*toward/toward.sqrMagnitude, rotation) as GameObject;
			aprojection.GetComponent<DestroyMudball>().Initialize(10);
			aprojection.GetComponent<Rigidbody2D>().AddForce(toward);
			
		}
	}
}
