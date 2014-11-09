using UnityEngine;
using System.Collections;

public class VolumeManager : MonoBehaviour {
	public enum TypeOfAudio { MASTER, EFFECTS, MUSIC };

	[System.Serializable]
	public struct AudioLevel {
		public TypeOfAudio type;
		public float audioVolume;
	}
	
	public AudioLevel[] levels = new AudioLevel[3];
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float getVolume(TypeOfAudio at) {
		foreach(AudioLevel level in levels) {
			if(level.type == at) {
				return level.audioVolume;
			}
		}
		return 0.0f;
	}
	
	public void setVolume(TypeOfAudio at, float newVolume) {
		int count = 0;
		foreach(AudioLevel level in levels) {
			if(level.type == at) {
				levels[count].audioVolume = newVolume;
			}
			count++;
		}
	}
}
