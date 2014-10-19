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
	
	void Update () 
	{
		if (!gameObject.GetComponent<Status>().isStunned){
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
			{
				anim.SetBool("d",true);
				rigidbody2D.transform.position += Vector3.up 
					* gameObject.GetComponent<Status> ().speed 
					* gameObject.GetComponent<Status> ().getSpeedx () 
					* Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
			{
				anim.SetBool("a", true);
				anim.SetBool("d", false);
				rigidbody2D.transform.position += Vector3.left 
					* gameObject.GetComponent<Status> ().speed 
					* gameObject.GetComponent<Status> ().getSpeedx () 
					* Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) 
			{
				if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.LeftArrow)) 
					anim.SetBool("d",true);
				rigidbody2D.transform.position += Vector3.down 
					* gameObject.GetComponent<Status> ().speed 
					* gameObject.GetComponent<Status> ().getSpeedx () 
					* Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
			{			
				anim.SetBool("d", true);
				anim.SetBool("a", false);
				rigidbody2D.transform.position += Vector3.right 
					* gameObject.GetComponent<Status> ().speed 
					* gameObject.GetComponent<Status> ().getSpeedx () 
					* Time.deltaTime;
			}
		}
		//if not moving
		if(!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.UpArrow) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.DownArrow) && !Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.RightArrow))
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
