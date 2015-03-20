using UnityEngine;
using System.Collections;

public class soundScript : MonoBehaviour {

	public AudioClip[] castSounds = new AudioClip[0];

	public AudioClip deathSound;
	public AudioClip gameOver;
	
	public AudioClip introMusic;
	public AudioClip gameMusic;
	public AudioClip creditsMusic;

	public AudioClip boss1Music;
	public AudioClip boss2Music;
	public AudioClip boss3Music;

	public float musicVolume = .7f;
	public float gameMusicVolume = .3f;
	public float bossMusicVolume = .5f;

	public float timer;
	public float time;

	public GameObject ORB;

	private enemyScript orb;

	void Awake ()
	{
		orb = ORB.GetComponent<enemyScript>();
	}

	// Use this for initialization
	void Start ()
	{
		audio.playOnAwake = true;

		if (Application.loadedLevel == 0)
		{
			audio.loop = false;

			audio.volume = musicVolume;

			audio.PlayOneShot(introMusic);
		}

		if (Application.loadedLevel == 1)
		{
			audio.loop = true;

			audio.volume = musicVolume;

			audio.clip = introMusic;
			
			audio.Play();
		}

		if (Application.loadedLevel == 2)
		{
			audio.loop = true;

			audio.volume = gameMusicVolume;

			audio.clip = gameMusic;
			
			audio.Play();
		}

		if (Application.loadedLevel == 3)
		{
			audio.loop = true;

			audio.volume = musicVolume;

			audio.clip = creditsMusic;
			
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (orb.currentBOSS == enemyScript.BOSS.CRAWLER)
		{
			if (orb.currentSTATE != enemyScript.STATE.IDLE)
			{
				if (audio.clip != boss1Music)
				{
					audio.clip = boss1Music;

					audio.volume = bossMusicVolume;

					audio.Play();
				}
			}
		}

		else if (orb.currentBOSS == enemyScript.BOSS.INSECT)
		{
			if (orb.currentSTATE != enemyScript.STATE.IDLE)
			{
				if (audio.clip != boss2Music)
				{
					audio.clip = boss2Music;

					audio.volume = bossMusicVolume;

					audio.Play();
				}
			}
		}

		else if (orb.currentBOSS == enemyScript.BOSS.OBSERVER)
		{
			if (orb.currentSTATE != enemyScript.STATE.IDLE)
			{
				if (audio.clip != boss3Music)
				{
					audio.clip = boss3Music;

					audio.volume = bossMusicVolume;

					audio.Play();
				}
			}
		}

		if (orb.currentSTATE == enemyScript.STATE.IDLE)
		{
			if (audio.clip != gameMusic)
			{
				audio.clip = gameMusic;
				
				audio.volume = gameMusicVolume;
				
				audio.Play();
			}
		}
	}
}