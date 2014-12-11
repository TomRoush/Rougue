using UnityEngine;
using System.Collections;

public class Heal : Spell<GameObject> {
	public GameObject heal;
	private int healthHealed;

    public Heal(GameObject pCaster) : base(pCaster)
    {
        name = "Heal";
		heal = Resources.Load("HealEffect") as GameObject;
		cooldownIcon = Resources.Load("Artwork/InGame/Heal") as Texture2D;
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
        target.GetComponent<Status>().healHealth( 20 + 0.5f*caster.GetComponent<Status>().getIntelligence() + 0.05f*caster.GetComponent<Status>().getMaxHealth());
        lastCastTime = Time.time;
		
		//Quaternion rotation = Quaternion.identity;
		//rotation.eulerAngles = new Vector3(270, 0, 0);
		GameObject healInstance = GameObject.Instantiate (heal, caster.transform.position, Quaternion.identity) as GameObject;
		if(Time.time - lastCastTime > 0.5f) {
			GameObject.Destroy(healInstance);
		}
    }
}
