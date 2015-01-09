using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyStatus : Status {

	public override void Start() {
		base.Start();

		level = Random.Range(floor, floor + 5);
		enemyLevelStats();

		player = GameObject.FindGameObjectWithTag("Player");
		healthSlider = (transform.Find("Canvas/Slider").gameObject as GameObject).GetComponent<Slider>();
	}

	protected override void takeHealth(float d) {
		// update health bars
		base.takeHealth(d);
		if(health < 0) {
			player.GetComponent<PlayerStatus>().killedEnemy(expDrop);
			Destroy(gameObject);
		}
	}
}