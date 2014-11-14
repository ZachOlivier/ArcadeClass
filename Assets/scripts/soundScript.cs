using UnityEngine;
using System.Collections;

public class soundScript : MonoBehaviour {

	public AudioClip deathSound;
	public AudioClip gameOver;

	public float timer;
	public float time;

	// Use this for initialization
	void Start () {
	
		audio.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
	

	}
}