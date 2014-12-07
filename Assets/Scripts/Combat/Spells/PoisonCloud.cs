using UnityEngine;
using System.Collections;

public class PoisonCloud : Spell<GameObject> {
	
	public GameObject poisonCloud;
	public GameObject player;
	//public LayerMask pcloudLayer = LayerMask.NameToLayer("Cloud");
	//public LayerMask everything = LayerMask.NameToLayer("Everything");
	
	public PoisonCloud(GameObject pCaster) : base(pCaster){
		name = "PoisonCloud";
		poisonCloud = Resources.Load ("PoisonCloud") as GameObject;
		cooldownIcon = Resources.Load("Artwork/InGame/PoisonCloud") as Texture2D;
	}

	protected override void RefreshValues()
	{
		switch (level)
		{
		case 1:
			manaCost = 15;
			coolDown = 7;
			break;
		case 2:
			manaCost = 12;
			coolDown = 5;
			break;
		default:
			Debug.Log("Fireball level error");
			break;
		}
		
	}

	protected override void CastSpell(GameObject closest)
	{
		player = caster;//GameObject.FindGameObjectWithTag ("Player");
		if (!player.GetComponent<Status>().isStunned){
			
			Quaternion rotation = Quaternion.identity;
			GameObject pcloud = GameObject.Instantiate (poisonCloud, player.transform.position, rotation) as GameObject;
			Physics2D.IgnoreLayerCollision(pcloud.layer, caster.layer);
			Physics2D.IgnoreLayerCollision(pcloud.layer, LayerMask.NameToLayer("Enemies"));
			Physics2D.IgnoreLayerCollision(pcloud.layer, LayerMask.NameToLayer("Projectile"));
			//Physics2D.IgnoreLayerCollision(pcloud.layer, LayerMask.NameToLayer("Clouds"));
		}
	}
}
