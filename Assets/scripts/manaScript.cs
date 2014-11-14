using UnityEngine;
using System.Collections;

public class manaScript : MonoBehaviour {

	public int manaStore;
	public float manaTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (manaStore < 5)
		{
			manaTime += Time.deltaTime;

			if (manaTime >= 1)
			{
				if (manaStore == 4)
				{
					manaStore += 1;
				}

				else
				{
					manaStore += 2;
				}

				manaTime = 0;
			}
		}

		else
		{
			if (manaTime != 0)
			{
				manaTime = 0;
			}
		}
	}
}