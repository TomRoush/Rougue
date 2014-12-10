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
        cooldownIcon = Resources.Load("Artwork/InGame/Fireball") as Texture2D;

    }    

    protected override void RefreshValues()
    {
        switch (level)
        {
            case 1:
                manaCost = 40;
                coolDown = 5;
                damage = 10;
			break;
            case 2:
                manaCost = 60;
                coolDown = 5;
                damage = 30;
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
			fball.GetComponent<DestroyFireball>().Initialize(damage+2*player.GetComponent<Status>().getIntelligence());
			fball.GetComponent<Rigidbody2D>().AddForce(toward);
			
		}
    }
}
