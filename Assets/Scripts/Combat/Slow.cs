using UnityEngine;
using System.Collections;

public class Slow : TimedEffect {

	protected override void ApplyEffect (){
		handleTargetNull ();
		target.GetComponent<Status> ().speedx -= 0.8f;
	}

	protected override void EndEffect (){
		handleTargetNull ();
		target.GetComponent<Status> ().speedx += 0.8f;
		base.EndEffect ();
	}

}
