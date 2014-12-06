using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {
	public string sortingLayerName;
	
	void Start () {
		transform.GetComponent<ParticleSystem>().renderer.sortingLayerName = sortingLayerName;
	}
}
