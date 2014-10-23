using UnityEngine;
using System.Collections;

public class Player : Entities 
{
	//public enumerator to easily communicate current game state
	public enum GameState { PLAYING, PAUSED };
	public static GameState playerState = GameState.PLAYING;

	public static bool paused = false;
	private bool a;
	private bool d;
	public float curHealth;
	
	Animator anim;
	
	// GUI
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(100,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private GUIStyle currentStyle = null;
	
	public ParticleSystem blood;//turned public

    public MakeMap Dungeon;

	void Start () {

		paused = false;
		anim = GetComponent<Animator> ();
		curHealth = health;//gameObject.GetComponent<Status> ().health?
		blood = transform.Find("Blood").GetComponent<ParticleSystem>();
        Dungeon = GameObject.Find("MapGenerator").GetComponent<MakeMap>();
    }
	
	void FixedUpdate () 
	{
		if (!gameObject.GetComponent<Status>().isStunned)
		{
			Vector3 v1 = Vector3.zero;//will change if move up/down is pressed
			Vector3 v2 = Vector3.zero;//will change if move left/right is pressed
			if (PlayerInput.isMovingUp()) 
			{
				anim.SetBool("d",true);
				v1 = Vector3.up;
			}
			if (PlayerInput.isMovingDown()) 
			{
				anim.SetBool("d",true);
				v1 = Vector3.down;
			}
			if (PlayerInput.isMovingLeft()) 
			{
				anim.SetBool("d",false);
				anim.SetBool("a",true);
				v2 = Vector3.left;
			}
			if (PlayerInput.isMovingRight()) 
			{
				anim.SetBool("d",true);
				anim.SetBool("a",false);
				v2 = Vector3.right;
			}
			if(v1!=Vector3.zero && v2!=Vector3.zero) moveDirection(v1, v2);//moves in an average of the two vectors directions with the same speed
			else if (v1!=Vector3.zero) moveDirection(v1);
			else moveDirection(v2);
		}
		//if not moving
		if(!PlayerInput.isMoving() || gameObject.GetComponent<Status>().isStunned)//not moving or is stunned *possibly make a stun animation?
		{
			anim.SetBool("d",false);
			anim.SetBool("a", false);
		}

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
		    Dungeon.NextFloor();	
            //Application.LoadLevel ("Game");
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

	public void Die()
	{
		print ("I've been killed");
		Debug.Break();
	}
}
