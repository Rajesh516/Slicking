using UnityEngine;
using System.Collections;

public enum SoundTypes { Crash, GameOver };

public class AudioManager : MonoBehaviour {
	public static AudioManager Instance;

	public AudioSource audiosrc1;
	public AudioSource audiosrc2;
	public AudioClip GameSound;
	public AudioClip HomeScreenSound;
	public AudioClip FailSound;
	public AudioClip GameOverSound;

	// Use this for initialization
	void Start () {
		Instance = this;
		PlayHomeScreenSound ();
	}

	public void PlayGameSound() {
		audiosrc1.clip = GameSound;
		audiosrc1.Play ();
	}

	public void PlayHomeScreenSound() {
		audiosrc1.clip = HomeScreenSound;
		audiosrc1.Play ();
	}

	public void playSound(SoundTypes types) {
		switch (types) {
		case SoundTypes.Crash : PlayOneShot(FailSound); break; 		
		case SoundTypes.GameOver : PlayOneShot(GameOverSound); break; 
		}
	}

	void PlayOneShot(AudioClip clip) {
		Debug.Log (clip.name);
		audiosrc2.PlayOneShot (clip, AudioListener.volume);
	}
}
