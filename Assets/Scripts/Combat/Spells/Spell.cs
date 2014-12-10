using UnityEngine;
using System.Collections;

public abstract class Spell <gType>
{

	protected int manaCost;
    protected string name;
    protected float coolDown;
    protected float lastCastTime;
    protected GameObject caster;
    protected Status casterStat;
    protected int level;
    protected int maxLevel;
    protected Texture2D cooldownIcon;
   

    public void cast(gType target)
    {
        if(CanCast())
        {
            CastSpell(target);
            EndCast();
        }
    }

    protected abstract void CastSpell(gType target);
    protected abstract void RefreshValues();
    
    public void Upgrade()
    {
        level++;
        RefreshValues();
    }
     
    public Spell(GameObject pCaster)
    {
		level = 1;
        caster = pCaster;
        lastCastTime = Time.time - coolDown - 1;
        casterStat = caster.GetComponent<Status>();
		RefreshValues ();
    }


    public bool CanCast()
    {
        return (casterStat.getMana() >= manaCost && OffCooldown());
    }


    public void EndCast()
    {
        lastCastTime = Time.time;
        casterStat.useMana(manaCost);
    }

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

	public Texture2D getCooldownIcon()
	{
		return cooldownIcon;
	}
}
