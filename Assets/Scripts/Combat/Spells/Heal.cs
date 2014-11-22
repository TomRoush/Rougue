using UnityEngine;
using System.Collections;

public class Heal : Spell<GameObject> {
	public GameObject heal;
	private int healthHealed;

    public Heal(GameObject pCaster) : base(pCaster)
    {
        manaCost = 20;
        name = "Heal";
        coolDown = 10;
		heal = Resources.Load("HealEffect") as GameObject;
    }

	protected override void RefreshValues()
	{
		switch (level)
		{
		case 1:
			manaCost = 20;
			coolDown = 10;
			healthHealed = 30;
			break;
		case 2:
			manaCost = 40;
			coolDown = 8;
			healthHealed = 50;
			break;
		default:
			Debug.Log("Heal level error");
			break;
		}
		
	}

    protected override void CastSpell(GameObject target)
    {
        target.GetComponent<Status>().health += 20 + 0.1f*caster.GetComponent<Status>().intelligence + 0.05f*caster.GetComponent<Status>().maxHealth;
        lastCastTime = Time.time;
		
		//Quaternion rotation = Quaternion.identity;
		//rotation.eulerAngles = new Vector3(270, 0, 0);
		GameObject healInstance = GameObject.Instantiate (heal, caster.transform.position, Quaternion.identity) as GameObject;
		if(Time.time - lastCastTime > 0.5f) {
			GameObject.Destroy(healInstance);
		}
    }
}
