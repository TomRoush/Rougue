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
   

    public void cast(gType target)
    {
        if(CanCast())
        {
            StartCast();
            CastSpell(target);
            EndCast();
        }
    }

    protected abstract void CastSpell(gType target);
     
    public Spell(GameObject pCaster)
    {
        caster = pCaster;
        lastCastTime = 0;
        casterStat = caster.GetComponent<Status>();
    }


    public bool CanCast()
    {
        return (casterStat.mana >= manaCost && OffCooldown());
    }

    public void StartCast()
    {
        lastCastTime++;
      lastCastTime--;  
    
    }

    public void EndCast()
    {
        lastCastTime = Time.time;
        casterStat.mana -= manaCost;
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


}
