using UnityEngine;
using System.Collections;

// Creates a lightning bolt like effect
public class Lightning : MonoBehaviour {
	public Transform target; // What we are hitting
	public static int segments = 10; // How many particles we will be using TODO Make this dynamic? Can we?
	public float speed = 1.0f; // How fast the particles move
	public float scale = 1.0f; // How much affect noise has
	Perlin noise; // The noise generator;
	private Particle[] particles; // Array of particles in the bolt
	public GameObject renderer;
	LineRenderer[] renderers = new LineRenderer[segments - 1];
	
	public float x = 0.1365143f;
	public float y = 1.21688f;
	public float z = 2.5564f;
	
	void Start () {
		noise = new Perlin();
		Debug.Log(renderers.Length);
		for(int i = 0; i < segments - 1; i++) {
			GameObject rendererObject = GameObject.Instantiate(renderer, Vector3.zero, Quaternion.identity) as GameObject;
			renderers[i] = rendererObject.GetComponent<LineRenderer>();
		}
		// Emit only what we need and store them to array
		particleEmitter.emit = false;
		particleEmitter.Emit(segments);
		particles = particleEmitter.particles;
	}
	
	void Update () {
		float timeX = 0; // Because!
		float timeY = Time.time * speed * y; // Magic semi-random numbers!
		float timeZ = Time.time * speed * z; // Magic semi-random number!
		
		for(int i = 0; i < particles.Length; i++) {
			// Put the particle next in line
			Vector3 position = Vector3.Lerp(transform.position, target.position, (float)i / (float)segments);
			Vector3 offset = new Vector3(noise.Noise(timeX + position.x, timeX + position.y, timeX + position.z),
			                             noise.Noise(timeY + position.x, timeY + position.y, timeY + position.z),
			                             noise.Noise(timeZ + position.x, timeZ + position.y, timeZ + position.z));
			position += (offset * scale * ((float)i / (float) segments));
			
			// Apply to the renderers
			if(i == 0) {
				renderers[i].SetPosition(0, Vector3.zero);
			} else {
				renderers[i - 1].SetPosition(0, position);
			}
			if(i != particles.Length - 1) {
				renderers[i].SetPosition(1, position);
			}
			
			// Apply properties to the particle
			particles[i].position = position;
			particles[i].color = Color.white;
			particles[i].energy = 1.0f;
		}
		
		particleEmitter.particles = particles;
	}
}
