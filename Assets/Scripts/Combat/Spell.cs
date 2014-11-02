using UnityEngine;
using System.Collections;

public abstract class Spell {

	protected int manaCost;
    protected string name;
    protected float coolDown;
    protected float lastCastTime;

    public abstract void cast(GameObject target);

    public int getCost()
    {
        return manaCost;
    }

    public string getName()
    {
        return name;
    }

    public float getCoolDown()
    {
        return coolDown;
    }

    public float getLastCastTime()
    {
        return lastCastTime;
    }

    public bool OffCooldown()
    {
        return Time.time > (lastCastTime + coolDown);
    }


}
