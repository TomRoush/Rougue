using UnityEngine;
using System.Collections;

public class PoisonCloud : Spell<GameObject> {
	
	public GameObject poisonCloud;
	public GameObject player;
	//public LayerMask pcloudLayer = LayerMask.NameToLayer("Cloud");
	//public LayerMask everything = LayerMask.NameToLayer("Everything");
	
	public PoisonCloud(GameObject pCaster) : base(pCaster){
		manaCost = 20;
		name = "PoisonCloud";
		coolDown = 8;
		poisonCloud = Resources.Load ("PoisonCloud") as GameObject;
	}
	
	protected override void CastSpell(GameObject closest)
	{
		player = caster;//GameObject.FindGameObjectWithTag ("Player");
		if (!player.GetComponent<Status>().isStunned){
			
			Quaternion rotation = Quaternion.identity;
			GameObject pcloud = GameObject.Instantiate (poisonCloud, player.transform.position, rotation) as GameObject;
			Physics2D.IgnoreLayerCollision(pcloud.layer, caster.layer);
			Physics2D.IgnoreLayerCollision(pcloud.layer, LayerMask.NameToLayer("Enemies"));
		}
	}
}
