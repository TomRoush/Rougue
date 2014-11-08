using UnityEngine;
using System.Collections;

public class Heal : Spell {
	public GameObject heal;

    public Heal(GameObject pCaster) : base(pCaster)
    {
        manaCost = 20;
        name = "Heal";
        coolDown = 10;
		heal = Resources.Load("HealEffect") as GameObject;
    }

    protected override void CastSpell(GameObject target)
    {
        target.GetComponent<Status>().health += 30;
        lastCastTime = Time.time;
		
		//Quaternion rotation = Quaternion.identity;
		//rotation.eulerAngles = new Vector3(270, 0, 0);
		GameObject healInstance = GameObject.Instantiate (heal, caster.transform.position, Quaternion.identity) as GameObject;
		if(Time.time - lastCastTime > 0.5f) {
			GameObject.Destroy(healInstance);
		}
    }
}
