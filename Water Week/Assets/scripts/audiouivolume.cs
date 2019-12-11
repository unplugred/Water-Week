using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiouivolume : MonoBehaviour
{
	[SerializeField] AudioSource sauce;
	[SerializeField] AudioSource opposite;
	[SerializeField] AnimationCurve cruve;
	float vol = 0;
	bool dir = true;
	[SerializeField] float master;

	void Update()
	{
		float f = cruve.Evaluate(vol = Mathf.Clamp01(vol + (dir ? 1.5f : -1.5f) * Time.deltaTime));
		sauce.volume = f*master;
		opposite.volume = Mathf.Min((1 - f)*.5f, opposite.volume);
	}

	public void flip()
	{
		dir = !dir;
	}
}
