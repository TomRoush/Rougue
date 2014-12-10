using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
	public VolumeManager.TypeOfAudio type = VolumeManager.TypeOfAudio.MASTER;
	VolumeManager manager;
	public AudioClip clip;
	AudioSource source;
	public bool playOnAwake;
	public bool loop;
	
	public AudioPlayer(GameObject go, AudioClip c, VolumeManager.TypeOfAudio audioType) {
		clip = c;
		type = audioType;
		manager = ((VolumeManager)FindObjectOfType(typeof(VolumeManager)));
		source = go.AddComponent<AudioSource>() as AudioSource;
		source.clip = clip;
	}
	
	// Use this for initialization
	void Start () {
		manager = ((VolumeManager)FindObjectOfType(typeof(VolumeManager)));
		source = gameObject.AddComponent<AudioSource>() as AudioSource;
		source.clip = clip;
//		if(playOnAwake) { this.play(); Debug.Log("Playing");}
		setLoop(loop);
	}
	
	public void play() {
		source.volume = (manager.getVolume(VolumeManager.TypeOfAudio.MASTER) + Mathf.Max(manager.getVolume(VolumeManager.TypeOfAudio.MASTER), manager.getVolume(type))) / 20.0f;
		Debug.Log(source.volume);
		source.Play();
	}
	
	public void playOneShot() {
		
	}
	
	public void setLoop(bool willLoop) {
		source.loop = willLoop;
	}
}