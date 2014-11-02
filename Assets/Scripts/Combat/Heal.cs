using UnityEngine;
using System.Collections;

public class Heal : Spell {


    public Heal()
    {
        manaCost = 20;
        name = "Heal";
        coolDown = 10;
        lastCastTime = 0;
    }

    public override void cast(GameObject target)
    {
        target.GetComponent<Status>().health += 30;
        lastCastTime = Time.time;
    }
}
