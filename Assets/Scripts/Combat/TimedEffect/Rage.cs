using UnityEngine;
using System.Collections;

public class Rage : TimedEffect {
	
	protected override void ApplyEffect (){
		handleTargetNull ();
		target.GetComponent<Status> ().speedx+=1f;
		target.GetComponent<Status> ().damagex+=5f;
		target.GetComponent<Status> ().attackSpeed+=1f;
		target.GetComponent<Status> ().isRaged = true;
	}
	
	protected override void EndEffect (){
		handleTargetNull ();
		target.GetComponent<Status> ().speedx-=1f;
		target.GetComponent<Status> ().damagex-=5f;
		target.GetComponent<Status> ().attackSpeed-=1f;
		target.GetComponent<Status> ().rage = 0;
		target.GetComponent<Status> ().isRaged = false;
		base.EndEffect ();
	}
	
}
