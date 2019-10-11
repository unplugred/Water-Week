using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlescreen : MonoBehaviour
{
	[SerializeField] bttn start;
	[SerializeField] Transform title;
	[SerializeField] GameObject namingscreen;
	[SerializeField] AnimationCurve scalex;
	[SerializeField] AnimationCurve scaley;
	float progress = 0;
	bool ideal = true;

	void OnEnable()
	{
		ideal = true;
		progress = 0;
	}

	void Update()
	{
		if((ideal && progress < 1) || (!ideal && progress > 0))
		{
			progress += Time.deltaTime * (ideal ? 1.5f : -1.5f);
			if(progress >= 1 && ideal)
			{
				progress = 1;
				start.ison = true;
			}
			else if(progress <= 0 && !ideal)
			{
				progress = 0;
				namingscreen.SetActive(true);
				gameObject.SetActive(false);
			}
			title.localScale = new Vector3(scalex.Evaluate(progress), scaley.Evaluate(progress), 1);
		}
	}

	public void flip()
	{
		ideal = !ideal;
	}
}
