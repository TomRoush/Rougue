using UnityEngine;
using System.Collections;

public class Player : Entities 
{

	//public enumerator to easily communicate current game state
	public enum GameState { PLAYING, PAUSED };
	public static GameState playerState = GameState.PLAYING;

	//replaced with Entities global speed
	//public float speed;
	//public float turnSpeed; Not used
	public static bool paused = false;
	private bool a;
	private bool d;
	public int curHealth;
	
	Animator anim;

	//private Vector3 moveDirection; Used for mouse move
	
	// GUI
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(100,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private GUIStyle currentStyle = null;
	
	ParticleSystem particle;

	void Start () {

		paused = false;
		anim = GetComponent<Animator> ();
		curHealth = health;
		particle = transform.Find("Blood").GetComponent<ParticleSystem>();

	}
	
	void Update () 
	{
		////Attempted mouse implementation
		/*Vector3 currentPosition = transform.position;
		Vector3 targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//targetPosition.z = 0;
		if (Input.GetButton("Fire1"))
		{
			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();
		}
		//while(Input.GetButton ("Fire1"))
		{
			//Vector3 target = moveDirection * moveSpeed + currentPosition;
			//if(!((targetPosition.x).Equals(this.rigidbody2D.position.x) && !(targetPosition.y).Equals(rigidbody2D.position.y)))
			//transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
		}
		//*/

		//consider taking out for less calls

		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
		{
			anim.SetBool("d",true);
			rigidbody2D.transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
		{
			anim.SetBool("a", true);
			anim.SetBool("d", false);
			rigidbody2D.transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) 
		{
			if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.LeftArrow)) 
				anim.SetBool("d",true);
			rigidbody2D.transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
		{			
			anim.SetBool("d", true);
			anim.SetBool("a", false);
			rigidbody2D.transform.position += Vector3.right * speed * Time.deltaTime;
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


		if(health <= 0)
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
		if(curHealth > health) {
			curHealth = health;
			particle.Play();
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
		GUI.BeginGroup(new Rect(0,0, health, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex, currentStyle);
		GUI.EndGroup();
		GUI.EndGroup();
		
		if (health<=0.0f){
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
		if (health < 50.0f) {
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
