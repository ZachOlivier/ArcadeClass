using UnityEngine;
using System.Collections;

public class spellScript : MonoBehaviour {

	public float moveSpeed;

	public Color player;

	public float changeSpeed;
	public float alpha;

	public bool alphaChanger;
	
	void Awake ()
	{

	}

	// Use this for initialization
	void Start ()
	{
		player = this.renderer.material.color;

		alpha = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!alphaChanger)
		{
			if (alpha <= 0)
			{
				alphaChanger = true;
			}

			else
			{
				alpha -= changeSpeed * Time.deltaTime;
				
				player.a = alpha;
				
				this.renderer.material.color = player;
			}
		}

		else if (alphaChanger)
		{
			if (alpha >= 1)
			{
				alphaChanger = false;
			}

			else
			{
				alpha += changeSpeed * Time.deltaTime;
				
				player.a = alpha;
				
				this.renderer.material.color = player;
			}
		}
	}
}