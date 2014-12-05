using UnityEngine;
using System.Collections;

public class Rage : TimedEffect {

    Status tStat;
	
	protected override void ApplyEffect (){
        tStat = target.GetComponent<Status>();

		handleTargetNull ();
        tStat.setSpeedx(tStat.getSpeedx() + 1);
		tStat.damagex+=5f;
		tStat.attackSpeed+=1f;
		tStat.isRaged = true;
	}
	
	protected override void EndEffect (){
		handleTargetNull ();
        tStat.setSpeedx(tStat.getSpeedx() - 1);
		tStat.damagex-=5f;
		tStat.attackSpeed-=1f;
		tStat.rage = 0;
		tStat.isRaged = false;
		base.EndEffect ();
	}
	
}
