using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakepos : MonoBehaviour
{
	[SerializeField]
	float shakeAmount = 0.15f;
	Vector3 startpos;
	float burstamnt;

	void Start()
	{
		startpos = transform.localPosition;
	}

	void Update()
	{
		transform.localPosition = startpos + new Vector3(
			Random.value - 0.5f,
			Random.value - 0.5f,
			Random.value - 0.5f) * (shakeAmount + burstamnt);
		burstamnt = Mathf.Lerp(burstamnt, 0, Time.deltaTime * 10);
	}

	public void burst(float amount)
	{
		burstamnt += amount;
	}
}
