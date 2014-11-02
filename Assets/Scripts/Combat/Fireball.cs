using UnityEngine;
using System.Collections;

public class Fireball : Spell {

	public float fireballTimer;
	public GameObject fireball;
	public GameObject closest;
	public GameObject player;


    public Fireball(GameObject pCaster) : base(pCaster)
    {
        manaCost = 50;
        name = "Fireball";
        coolDown = 4;
        fireball = Resources.Load("Fireball") as GameObject;
    }    

    protected override void CastSpell(GameObject closest)
    {
		player = caster;//GameObject.FindGameObjectWithTag ("Player");

			float bulletspeed = 1800f;
			//float xval = (closest.transform.position.x - player.transform.position.x) ;
			//float yval = (closest.transform.position.y - player.transform.position.y) ;
			float xval = (closest.transform.position.x - player.transform.position.x) ;
			float yval = (closest.transform.position.y - player.transform.position.y) ;
			Vector3 toward = new Vector3(xval * bulletspeed, yval * bulletspeed, 1.9f)/caster.GetComponent<Status>().getDistance(closest);

			GameObject fball = GameObject.Instantiate (fireball, player.transform.position + 100*toward/toward.sqrMagnitude, Quaternion.identity) as GameObject;
			fball.GetComponent<Rigidbody2D>().AddForce(toward);

            lastCastTime = Time.time;
    }
    /*
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		fireballTimer = 0; 
		//parent = transform.parent;
		//fireball = (GameObject) Resources.Load ("Fireball");

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.F) && fireballTimer<=0 
		  && player.GetComponent<Status>().mana > 50 && player.GetComponent<Status>().health > 20) {
			player.GetComponent<Status>().mana -= 50;
			player.GetComponent<Status>().health -= 20;
			closest = player.GetComponent<Status>().FindClosestEnemy();
			Debug.Log("fire");

			//Vector3 vpp = Camera.main.ScreenToViewportPoint(Input.mousePosition);

			float bulletspeed = 1800f;
			//float xval = (closest.transform.position.x - player.transform.position.x) ;
			//float yval = (closest.transform.position.y - player.transform.position.y) ;
			float xval = (closest.transform.position.x - player.transform.position.x) ;
			float yval = (closest.transform.position.y - player.transform.position.y) ;
			Vector3 toward = new Vector3(xval * bulletspeed, yval * bulletspeed, 1.9f)/GetComponent<Status>().getDistance(closest);

			Rigidbody2D fball = Instantiate (fireball, player.transform.position + 100*toward/toward.sqrMagnitude, Quaternion.identity) as Rigidbody2D;
			fball.AddForce(toward);
			fireballTimer=4f;
		}
		if (Input.GetKey (KeyCode.M) && fireballTimer<=0) {
			closest = player.GetComponent<Status>().FindClosestEnemy();
			Debug.Log("fire");
			
			Rigidbody2D fball = Instantiate (fireball, player.transform.position, Quaternion.identity) as Rigidbody2D;

			fireballTimer=4;
		}
		if (fireballTimer>0){
			fireballTimer-=Time.deltaTime;
		}
	}
    */
}
