using UnityEngine;
using System.Collections;

public class Heal : Spell {


    public Heal(GameObject pCaster) : base(pCaster)
    {
        manaCost = 20;
        name = "Heal";
        coolDown = 10;
    }

    protected override void CastSpell(GameObject target)
    {
        target.GetComponent<Status>().health += 30;
        lastCastTime = Time.time;
    }
}
