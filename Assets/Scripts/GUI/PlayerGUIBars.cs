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

    private int TotalX;
   private int TotalY;
  private int TotalW;
 private int TotalH; 
 public float pWAction;// = 0.5f;
 public float pWGap; //= 0.05f; //space between bars
 public float pHGap;
 public float pWOGap; //= 0.05f; //space between bars and edge of screen
 public float pHOGap;
 public float pHBars; // = 0.9f; // p for percent
 public float pHT;

 public float pXPH;
 
 public float pHAction;

 public int getBX()
 {
    return (int) (TotalX+TotalW/2 - TotalW*pWAction/2);
 }

 public int getBY()
 {

     return (int) (TotalY + (TotalH - pHAction*TotalH - pHGap*TotalH));
 }
 public int getBW()
 {
    return (int) (TotalW * pWAction);
 }
 public int getBH()
 {
    return (int) (TotalH*pHAction);
 }
 void Awake() {
     TotalX = Screen.width / 4;
     TotalW = Screen.width/2;
     TotalH = (int) (Screen.height * pHT);
     TotalY = Screen.height - TotalH;
 }


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
		var healthBarWidth = TotalW *(1-pWAction - 2*pWGap - 2*pWOGap)/2;
		var healthBarHeight = TotalH * (1-2*pHOGap);
		var currentHealthWidth = healthFraction * healthBarWidth;
        float hpX = TotalW * pWOGap;
        float hpY = TotalH * pHOGap;
		GUI.DrawTexture(new Rect(hpX+TotalX, hpY+TotalY, healthBarWidth, healthBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(hpX+TotalX, hpY+TotalY, currentHealthWidth, healthBarHeight), healthBarTexture, ScaleMode.StretchToFill);
	}

	void DrawManaBar(){
		manaFraction = (float)(pStat.getPercentMana()/100f);
		var manaBarWidth = TotalW *(1-pWAction - 2*pWGap - 2*pWOGap)/2;
		var manaBarHeight = TotalH * (1-2*pHOGap-pHGap)/2;
		var currentManaWidth = manaFraction * manaBarWidth;
        float manaX = TotalW - (TotalW*pWOGap) - manaBarWidth;
        float manaY = (TotalH * pHOGap);
		GUI.DrawTexture(new Rect(manaX + TotalX, TotalY+manaY, manaBarWidth, manaBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(TotalX + manaX, TotalY + manaY, currentManaWidth, manaBarHeight), manaBarTexture, ScaleMode.StretchToFill);
	}

	void DrawRageBar(){
		rageFraction = (float)(pStat.getPercentRage()/100f);//???
		var rageBarWidth = TotalW *(1-pWAction - 2*pWGap - 2*pWOGap)/2;
		var rageBarHeight = TotalH * (1-2*pHOGap-pHGap)/2;
		var currentRageWidth = rageFraction * rageBarWidth;
        float rageX = TotalW - (TotalW*pWOGap) -  rageBarWidth;
        float rageY = (TotalH *pHOGap +  pHGap*TotalH + rageBarHeight);
		GUI.DrawTexture(new Rect(rageX + TotalX, rageY+TotalY, rageBarWidth, rageBarHeight), outlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(rageX + TotalX, rageY+TotalY, currentRageWidth, rageBarHeight), rageBarTexture, ScaleMode.StretchToFill);
	}

	void DrawExpBar(){
		expFraction = (float)(pStat.getPercentExp () / 100f);
		var expBarWidth = getBW();
		var expBarHeight = TotalH * pXPH;
		var currentExpWidth = expFraction * expBarWidth;
        float xpX =  getBX();
        float xpY =  TotalH * pHGap;
		GUI.DrawTexture(new Rect(xpX , xpY + TotalY, expBarWidth, expBarHeight), expOutlineTexture, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(xpX , xpY + TotalY, currentExpWidth, expBarHeight), expBarTexture, ScaleMode.StretchToFill);
	}

	void DrawBackground(){
		
		GUI.DrawTexture(new Rect(TotalX, TotalY, TotalW, TotalH), finalbackground, ScaleMode.StretchToFill);
	}
}
