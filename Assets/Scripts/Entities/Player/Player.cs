using UnityEngine;
using System.Collections;

public class Player : Entities 
{
	//public enumerator to easily communicate current game state
	public enum GameState { PLAYING, PAUSED };
	public static GameState playerState = GameState.PLAYING;

	public static bool paused = false;
	private int previousDirection;
	private float velocity;
	public float curHealth;
	
	Animator anim;

	Transform weapon;
	
	// GUI
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(100,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private GUIStyle currentStyle = null;

	public ParticleSystem blood;//turned public

	void Start () {
        
        InitializeEntity();

		paused = false;
		anim = GetComponent<Animator> ();
		curHealth = health;//gameObject.GetComponent<Status> ().health?
		blood = transform.Find("Blood").GetComponent<ParticleSystem>();

		weapon = transform.Find ("Weapon");
	}

	void FixedUpdate () 
		//walkDirection: 1 = left, 2 = up, 3 = right, 4 = down;
		//idleDirection: saves previous walkDirection to animate idle
	{
        float dx = 0;
        float dy = 0;
		if (!gameObject.GetComponent<Status>().isStunned){
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
			{
				anim.SetInteger ("direction", 2);
				anim.SetFloat ("velocity", 1.0f);
				previousDirection = 2;
                dy = 1;
			}
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
			{
				//anim.SetBool("a", true);
				//anim.SetBool("d", false);

				anim.SetInteger ("direction", 1);
				anim.SetFloat ("velocity", 1.0f);
				previousDirection = 1;
                dx = -1;
			}
			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) 
			{
				if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.LeftArrow)) 
					anim.SetInteger ("direction", 4);
				anim.SetFloat ("velocity", 1.0f);
				previousDirection = 4;
                dy = -1;
			}
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
			{			
				Vector3 theScale = transform.localScale;
				theScale.x = 1;
				transform.localScale = theScale;

				anim.SetInteger ("direction", 3);
				anim.SetFloat ("velocity", 1.0f);
				previousDirection = 3;
                dx = 1;
			}
		}
		//if not moving
		if(!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.UpArrow) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.DownArrow) && !Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.RightArrow))
		{
			anim.SetInteger ("direction", previousDirection);
			anim.SetFloat ("velocity", 0.0f);
		}

        this.setDirection(new Vector3(dx,dy,0));
        Move();

		if(Input.GetKeyDown (KeyCode.Escape)) 
		{
			if(paused)
			{
				paused = false;
			}
			else
			{
				paused = true;
			}
		}


		if(gameObject.GetComponent<Status> ().health <= 0)
		{
			Die();		
		}


		UpdateGameState();
		updateHealth();
	}

	//NEEDS TO BE CALLED
	
	void UpdateGameState()
	{
		Time.timeScale = paused ? 0 : 1;
		
		playerState = paused ? GameState.PAUSED : GameState.PLAYING;

	}
	
	public void updateHealth()
	{
		if(curHealth > gameObject.GetComponent<Status> ().health) {
			curHealth = gameObject.GetComponent<Status> ().health;
			blood.Play();//moved to Status
		}
	}

	void OnGUI() 
	{
		if (paused) 
		{
			if(GUI.Button (new Rect((Screen.width)/2, ((Screen.height)/2)-50, 100, 50), "CONTINUE")) 
			{
				paused = false;
				//	Debug.Log("BUTTON HIT");
			}
			if(GUI.Button (new Rect((Screen.width)/2, ((Screen.height)/2)+50, 100, 50), "SAVE & QUIT")) 
			{
				paused = true;
				Application.LoadLevel("MainMenu");
			}
		}
		
		//draw the background:
		InitStyles ();
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, gameObject.GetComponent<Status> ().health, size.y));//gameObject.GetComponent<Status> ().health?
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex, currentStyle);
		GUI.EndGroup();
		GUI.EndGroup();
		
		if (gameObject.GetComponent<Status> ().health<=0.0f){
			GUI.Box (new Rect(Screen.width/2,Screen.height/2,100,50),"You died");
		}
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if(other.CompareTag("goal")) 
		{
			Application.LoadLevel ("Game");
		} else
		{
			Application.LoadLevel ("MainMenu");
		}
	}
	
	private void InitStyles()
	{
		if (currentStyle == null) {
			currentStyle = new GUIStyle (GUI.skin.box);
			currentStyle.normal.background = MakeTex (2, 2, new Color (0f, 1f, 0f, 1f));
		}
		if (gameObject.GetComponent<Status> ().health < 50.0f) {
			currentStyle.normal.background = MakeTex (2, 2, new Color (1f, 0f, 0f, 1f));
		}
	}
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}




	public void Respawn(Vector3 spawnPt)
	{
		transform.position = spawnPt;
	}

	public override void Die()
	{
		print ("I've been killed");
		Debug.Break();
	}
}
