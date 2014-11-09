using UnityEngine;
using System.Collections;

public class Port : Spell {
	
	public GameObject player;
	
	public Port(GameObject pCaster) : base(pCaster){
		manaCost = 50;
		name = "Port";
		coolDown = 12 + 1000f/player.GetComponent<Status>().intelligence;
	}
	
	protected override void CastSpell(GameObject closest){
		player = caster;
		if (!player.GetComponent<Status>().isStunned){
			if (Input.GetKeyDown (KeyCode.A)){
				//player.
			}
		}
	}

}
