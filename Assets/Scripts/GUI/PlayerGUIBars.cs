using UnityEngine;
using System.Collections;

public class PlayerGUIBars : MonoBehaviour {

	public Texture2D healthBarTexture;
	public Texture2D manaBarTexture;
	public Texture2D rageBarTexture;
	public Texture2D expBarTexture;
	public Texture2D outlineTexture;
	public Texture2D expOutlineTexture;
    public Texture2D finalbackground;
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

        DrawBackground();
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
        int hpX = Screen.width / 4;
        int hpY = Screen.height - 10 - (int )healthBarHeight;
		GUI.DrawTexture(new Rect(hpX, hpY, healthBarWidth, healthBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(hpX, hpY, currentHealthWidth, healthBarHeight), healthBarTexture, ScaleMode.StretchToFill);
	}

	void DrawManaBar(){
		manaFraction = (float)(pStat.getPercentMana()/100f);
		var manaBarWidth = manaBarTexture.width * scaleFactor;
		var manaBarHeight = manaBarTexture.height * scaleFactor *0.75f ;
		var currentManaWidth = manaFraction * manaBarWidth;
        int manaX = 3* Screen.width / 4 - (int) manaBarWidth;
        int manaY = Screen.height - 6 - (int )manaBarHeight * 2;
		GUI.DrawTexture(new Rect(manaX, manaY, manaBarWidth, manaBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(manaX, manaY, currentManaWidth, manaBarHeight), manaBarTexture, ScaleMode.StretchToFill);
	}

	void DrawRageBar(){
		rageFraction = (float)(pStat.getPercentRage()/100f);//???
		var rageBarWidth = rageBarTexture.width * scaleFactor;
		var rageBarHeight = rageBarTexture.height * scaleFactor * 0.75f;
		var currentRageWidth = rageFraction * rageBarWidth;
        int rageX = 3* Screen.width / 4 - (int) rageBarWidth;
        int rageY = Screen.height - 3 - (int) rageBarHeight;
		GUI.DrawTexture(new Rect(rageX, rageY, rageBarWidth, rageBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(rageX, rageY, currentRageWidth, rageBarHeight), rageBarTexture, ScaleMode.StretchToFill);
	}

	void DrawExpBar(){
		expFraction = (float)(pStat.getPercentExp () / 100f);
		var expBarWidth = expBarTexture.width * 5f;
		var expBarHeight = expBarTexture.height ;
		var currentExpWidth = expFraction * expBarWidth;
        int xpX =  Screen.width / 2  - (int) expBarWidth/2;
        int xpY = Screen.height - 50  - (int) expBarHeight;
		GUI.DrawTexture(new Rect(xpX, xpY, expBarWidth, expBarHeight), expOutlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(xpX, xpY, currentExpWidth, expBarHeight), expBarTexture, ScaleMode.StretchToFill);
	}

	void DrawBackground(){
        int X =  Screen.width / 4  - 10;
        int Y = Screen.height - 60;
		var BarWidth = finalbackground.width * 0.75f ;
		var BarHeight = 60;
		
		GUI.DrawTexture(new Rect(X, Y, BarWidth, BarHeight), finalbackground, ScaleMode.StretchToFill);
	}
}
