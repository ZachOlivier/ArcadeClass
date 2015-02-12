using UnityEngine;
using System.Collections;

public class soundScript : MonoBehaviour {

	public AudioClip[] castSounds = new AudioClip[0];

	public AudioClip deathSound;
	public AudioClip gameOver;

	private bool audioPlayed = false;

	public float timer;
	public float time;

	// Use this for initialization
	void Start () {
	
		audio.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!audioPlayed)
		{
			audio.PlayOneShot(castSounds[0]);

			audioPlayed = true;
		}
	}
}