using UnityEngine;
using System.Collections;

public class DoT : TimedEffect {

	public int damage;

	protected override void ApplyEffect() {
		handleTargetNull ();
		target.GetComponent<Status> ().MagicDamage(damage);
	}
}
