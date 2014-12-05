using UnityEngine;
using System.Collections;

public class Slow : TimedEffect {

        Status tStat;
	protected override void ApplyEffect (){
        tStat = target.GetComponent<Status>();
        tStat.setSpeedx(tStat.getSpeedx() - 0.8f);
		handleTargetNull ();
	}

	protected override void EndEffect (){
		handleTargetNull ();
        tStat.setSpeedx(tStat.getSpeedx() + 0.8f);
		base.EndEffect ();
	}

}
