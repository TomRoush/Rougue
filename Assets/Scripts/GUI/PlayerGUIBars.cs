using UnityEngine;
using System.Collections;

public class PlayerGUIBars : MonoBehaviour {

	public Texture2D healthBarTexture;
	public Texture2D manaBarTexture;
	public Texture2D rageBarTexture;
	public Texture2D expBarTexture;
	public Texture2D outlineTexture;
	public Texture2D expOutlineTexture;
	private float healthFraction;
	private float manaFraction;
	private float rageFraction;
	private float expFraction;
	private GUIStyle currentStyle = null;
	private Vector2 pos = new Vector2(20,40);
	private Vector2 size = new Vector2(100,20);
	private float aboveHeadOffset = 10;
	private float scaleFactor = .2f;
    private Status pStat;

    

	void OnGUI(){
        if(pStat == null)
            pStat = gameObject.GetComponent<Status>();

		DrawHealthBar ();
		DrawManaBar ();
		DrawRageBar ();
		DrawExpBar ();
		
	}
	void DrawHealthBar(){
		healthFraction = (float)(pStat.getPercentHealth()/100f);
		var healthBarWidth = healthBarTexture.width * scaleFactor;
		var healthBarHeight = healthBarTexture.height * scaleFactor;
		var currentHealthWidth = healthFraction * healthBarWidth;
		GUI.DrawTexture(new Rect(0, aboveHeadOffset, healthBarWidth, healthBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(0, aboveHeadOffset, currentHealthWidth, healthBarHeight), healthBarTexture, ScaleMode.StretchToFill);
	}

	void DrawManaBar(){
		manaFraction = (float)(pStat.getPercentMana()/100f);
		var manaBarWidth = manaBarTexture.width * scaleFactor;
		var manaBarHeight = manaBarTexture.height * scaleFactor;
		var currentManaWidth = manaFraction * manaBarWidth;
		GUI.DrawTexture(new Rect(0, aboveHeadOffset + healthBarTexture.height*scaleFactor, manaBarWidth, manaBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(0, aboveHeadOffset + healthBarTexture.height*scaleFactor, currentManaWidth, manaBarHeight), manaBarTexture, ScaleMode.StretchToFill);
	}

	void DrawRageBar(){
		rageFraction = (float)(pStat.getPercentRage()/100f);//???
		var rageBarWidth = rageBarTexture.width * scaleFactor;
		var rageBarHeight = rageBarTexture.height * scaleFactor;
		var currentRageWidth = rageFraction * rageBarWidth;
		GUI.DrawTexture(new Rect(0, aboveHeadOffset + 2*healthBarTexture.height*scaleFactor, rageBarWidth, rageBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(0, aboveHeadOffset + 2*healthBarTexture.height*scaleFactor, currentRageWidth, rageBarHeight), rageBarTexture, ScaleMode.StretchToFill);
	}

	void DrawExpBar(){
		expFraction = (float)(pStat.getPercentExp () / 100f);
		var expBarWidth = expBarTexture.width * 3.5f;
		var expBarHeight = expBarTexture.height ;
		var currentExpWidth = expFraction * expBarWidth;
		GUI.DrawTexture(new Rect(10, aboveHeadOffset + 3*healthBarTexture.height*scaleFactor, expBarWidth, expBarHeight), expOutlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(10, aboveHeadOffset + 3*healthBarTexture.height*scaleFactor, currentExpWidth, expBarHeight), expBarTexture, ScaleMode.StretchToFill);
	}

}
