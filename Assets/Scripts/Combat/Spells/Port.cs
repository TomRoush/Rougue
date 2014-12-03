using UnityEngine;
using System.Collections;

public class Port : Spell<GameObject> {
	
	public GameObject player;
	
	public Port(GameObject pCaster) : base(pCaster){
		name = "Port";
	}

	protected override void RefreshValues()
	{
		switch (level)
		{
		case 1:
			manaCost = 50;
			coolDown = 30;
			break;
		case 2:
			manaCost = 60;
			coolDown = 20;
			break;
		default:
			Debug.Log("Fireball level error");
			break;
		}
		
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
