using UnityEngine;
using System.Collections;

public class BuffRegen : TimedEffect {
	
	public float healthBuff;
    public float manaBuff;
    Status tStat;	
	protected override void ApplyEffect ()
	{
        tStat = target.GetComponent<Status>();
        tStat.modManaRegen += manaBuff;
        tStat.modHealthRegen += healthBuff;
        tStat.refreshStats();
	}
	
	protected override void EndEffect ()
	{
        tStat.modManaRegen -= manaBuff;
        tStat.modHealthRegen -= healthBuff;
        tStat.refreshStats();
		base.EndEffect ();
	}
}
