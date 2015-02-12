using UnityEngine;
using System.Collections;

public class manaScript : MonoBehaviour {

	public int manaStored;
	public int maxMana;

	public int manaReturned;

	public float manaTime;
	public float manaCD;

	// Use this for initialization
	void Start () {
	
		manaStored = maxMana;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (manaStored < maxMana)
		{
			manaTime += Time.deltaTime;

			if (manaTime >= manaCD)
			{
				if (manaStored == (maxMana - 1))
				{
					manaStored++;
				}

				else
				{
					manaStored += manaReturned;
				}

				manaTime = 0;
			}
		}

		if (manaStored < 0)
		{
			manaStored = 0;
		}

		if (manaStored > maxMana)
		{
			manaStored = maxMana;
		}
	}
}