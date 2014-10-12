using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour {
	// Use this for initialization
	void Start () {
		animation.wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	public void animate (string key) {
		if (key=="D") 
		{
			animation.CrossFade("MageWalking");
		}else{
			animation.CrossFade("MageIdle");
		}
	}
}
