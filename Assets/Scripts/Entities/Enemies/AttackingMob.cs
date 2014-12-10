using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackingMob : Entities {

    public GameObject attackingg;
    public Entities attacking;
    public int distance;
    public int lowRangeDamage;
    public int highRangeDamage;
    public LayerMask Wall;

    public float chaseTimeOut;
    private bool canAttack;
    private bool inRange;
    private bool visible;

    private MovementAI ai;
    private AIPath path;
    private Vector3 target;
    private Vector3 aidirection;
    private Vector3 randomTarget;
    private bool isWandering;
    private bool isBlindlyChasing;
    private float lastTimeSeen;

    private int halfwayPoint;

    private MakeMap mapgen;

    public string name;

    public int initFloor;


    void Start () {
        InitializeEntity();
        AutoTarget = new AstralProjection (gameObject);

        canAttack = true;
        if (attackingg == null) 
        {
            attackingg = GameObject.FindGameObjectWithTag("Player");
            attacking = attackingg.GetComponent<Entities>();
        }

        mapgen = GameObject.FindGameObjectWithTag("MapGen").GetComponent<MakeMap>();
        initFloor = mapgen.DungeonFloor;
        ai = new MovementAI (mapgen.currentFloor ());
        isWandering = true;
        isBlindlyChasing = false;
        /*
           path = ai.getPath (gameObject.transform.position, attackingg.transform.position);
           ai.currentNode = path.pop ();
           */
    }


    void FixedUpdate () {
        ai.fpscounter++;
        aidirection = attackingg.transform.position - gameObject.transform.position;

        RaycastHit2D hit = Physics2D.Raycast (gameObject.transform.position, aidirection, 12.0f, Wall);

        if (hit.collider != null && hit.collider.tag == "Player") {
            visible = true;
        } else {
            visible = false;
        }

        if(visible)
        {
            if(getDistance(attackingg) < 10.0f)
                    {
            this.setDirection(aidirection);
            Move();
            isBlindlyChasing = false; //name innacurate because of this, but #YOLO
            }
            else
            {
                isBlindlyChasing = true;
            }
            if(isWandering && getDistance(attackingg) >= 10.0f)
            {
                actuallyRePath(attackingg.transform.position);
            }
  
            isWandering = false;
            lastTimeSeen = Time.time;
        }
        else if(!visible && (Time.time - lastTimeSeen < chaseTimeOut))
        {
            isBlindlyChasing = true;
            isWandering = false;
        }
        else
        {
            if(isBlindlyChasing)
            {
                actuallyRePath(attackingg.transform.position);
            }
            isWandering = true;
            isBlindlyChasing = false;
        }



        if(isWandering)
        {
            if(ai.currentNode == null)
            {
                eTile[,] map = mapgen.currentFloor();
                Location randomPlace;	
                do{
                    randomPlace = new Location(Random.Range (0,map.GetLength(0)),Random.Range (0,map.GetLength(1)));
                }while (map[randomPlace.x, randomPlace.y] != eTile.Floor);

                randomTarget = new Vector3 (randomPlace.x, randomPlace.y, 0);

                PathFindTowards(randomTarget);
            } else
            {
                PathFindTowards(randomTarget);
            }
        }

        if(isBlindlyChasing)
        {
            PathFindTowards(attackingg.transform.position);
        }






        if (name.Equals("Ghost") && getDistance (attackingg) < 60 && GetComponent<Status>().see) {
            //if (getDistance (attackingg) < 60 && GetComponent<Status>().see) {
            AutoTarget.cast(attackingg);
            //Debug.Log ("123");
            //Debug.Log(attackingg.tag);
        }
        }

        public void actuallyRePath(Vector3 place)
        {
                if(MovementAI.lastRepathTime - Time.time < 0.10)
                {
            ai.fpscounter = 0;
            path = ai.getPath(gameObject.transform.position,place);
            path.pop();
            ai.currentNode = path.pop();
            halfwayPoint = path.length()/2;
                }
        }
        

        public void PathFindTowards(Vector3 place)
        {
            if ((halfwayPoint < 5 && ai.fpscounter > ai.fpsreset) || (halfwayPoint >= 5 && path.length() < halfwayPoint)) {
                actuallyRePath(place);
            }

            if (Vector3.SqrMagnitude (this.transform.position - target) < 0.01) { //Move on to next node
                this.rigidbody2D.velocity = new Vector2 (0, 0);
                ai.currentNode = path.pop ();
            }

            if (ai.currentNode != null) { // Move to current target node
                target = new Vector3 (ai.currentNode.loc.x, ai.currentNode.loc.y, 0);
                this.setDirection (target - this.transform.position);
                Move ();
            }
        }

        public float getDistance(GameObject go){
            return (go.transform.position - gameObject.transform.position).sqrMagnitude;
        }
    }
