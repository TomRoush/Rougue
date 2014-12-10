using UnityEngine;
using System.Collections;

public class Rage : TimedEffect {

    Status tStat;
	
	protected override void ApplyEffect (){
        tStat = target.GetComponent<Status>();

		handleTargetNull ();
        tStat.setSpeedx(tStat.getSpeedx() + 0.6f);
		tStat.damagex+=1.5f;
		tStat.modAttackSpeed+=1.5f;
		tStat.modHealthRegen+=1.5f;
		tStat.modManaRegen += 1.5f;
		tStat.isRaged = true;
        tStat.refreshStats();
	}
	
	protected override void EndEffect (){
		handleTargetNull ();
        tStat.setSpeedx(tStat.getSpeedx() - 0.6f);
		tStat.damagex-=1.5f;
		tStat.modAttackSpeed-=1.5f;
		tStat.modHealthRegen-=1.5f;
		tStat.modManaRegen -= 1.5f;
		tStat.rage = 0;
		tStat.isRaged = false;
        tStat.refreshStats();
		base.EndEffect ();
	}
	
}
