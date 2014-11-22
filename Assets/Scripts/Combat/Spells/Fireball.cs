using UnityEngine;
using System.Collections;

public class Fireball : Spell<GameObject> {

	//public float fireballTimer;
	public GameObject fireball;
	public GameObject closest;
	public GameObject player;
    private int damage;

    public Fireball(GameObject pCaster) : base(pCaster)
    {
        name = "Fireball";
        fireball = Resources.Load("Fireball") as GameObject;

    }    

    protected override void RefreshValues()
    {
        switch (level)
        {
            case 1:
                manaCost = 40;
                coolDown = 5;
                damage = 40;
			break;
            case 2:
                manaCost = 60;
                coolDown = 5;
                damage = 60;
			break;
            default:
                Debug.Log("Fireball level error");
			break;
        }

    }

    protected override void CastSpell(GameObject closest)
    {
		player = caster;//GameObject.FindGameObjectWithTag ("Player");
		if (!player.GetComponent<Status>().isStunned && closest != null){
			float bulletspeed = 1800f;
			//float xval = (closest.transform.position.x - player.transform.position.x) ;
			//float yval = (closest.transform.position.y - player.transform.position.y) ;
			float xval = (closest.transform.position.x - player.transform.position.x) ;
			float yval = (closest.transform.position.y - player.transform.position.y) ;
			Vector3 toward = new Vector3(xval * bulletspeed, yval * bulletspeed, 1.9f)/caster.GetComponent<Status>().getDistance(closest);

			float angle = Mathf.Atan2(yval, xval) * 180 / (Mathf.PI) - 90;
			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(0, 0, angle);
			GameObject fball = GameObject.Instantiate (fireball, player.transform.position + 100*toward/toward.sqrMagnitude, rotation) as GameObject;
			fball.GetComponent<Rigidbody2D>().AddForce(toward);
			
		}
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
		if (!player.GetComponent<Status>().isStunned && Input.GetKey (KeyCode.F) && fireballTimer<=0 
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
			Debug.Log("mine");
			
			Rigidbody2D fball = Instantiate (fireball, player.transform.position, Quaternion.identity) as Rigidbody2D;

			fireballTimer=4;
		}
		if (fireballTimer>0){
			fireballTimer-=Time.deltaTime;
		}
	}
    */
}
