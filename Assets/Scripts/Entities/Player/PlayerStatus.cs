using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : Status {

	// UI sliders
	private Slider manaSlider;
	private Slider rageSlider;
	private Slider expSlider;

	public override void Start() {
		base.Start();

		// sliders
		healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
		manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();
		rageSlider = GameObject.Find("RageSlider").GetComponent<Slider>();
		expSlider = GameObject.Find("XpSlider").GetComponent<Slider>();
	}

	public override void Update() {
		base.Update();

		// Update GUI
		manaSlider.value = (float)(getPercentMana() / 100f);
		rageSlider.value = (float)(getPercentRage() / 100f);
	}

	protected override void takeHealth(float d) {
		// blood
		// update health bars
		base.takeHealth(d);
	}

	public void killedEnemy(int expDrop) {
		money += level + expDrop;//don't think the (Clone) part is needed, but I put anyways; could also maybe find by tag "Player"
		exp += level + expDrop;

		if(exp > 100 + 10 * level) {
			exp -= 100 + 10 * level;
			level++;
			baseStrength += strengthGain;
			baseIntelligence += intelligenceGain;
			baseAgility += agilityGain;
			upgradePoints++;
			refreshStats();
			;
		}
		expSlider.value = (float)(getPercentExp() / 100f);
	}
}