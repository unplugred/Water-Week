using UnityEngine;

public class titlescreen : MonoBehaviour
{
	[SerializeField] mov start;
	[SerializeField] Transform title;
	[SerializeField] GameObject nextscreen;
	[SerializeField] AnimationCurve scalex;
	[SerializeField] AnimationCurve scaley;
	[SerializeField] AudioSource sauce;
	[SerializeField] AudioClip pancake;
	float progress = 0;
	bool ideal = true;

	void OnEnable()
	{
		ideal = true;
		progress = -.5f;
	}

	void Update()
	{
		if((ideal && progress < 1) || (!ideal && progress > 0))
		{
			bool beforetime = progress <= 0;
			progress += Time.deltaTime * (ideal ? .8f : -.8f);
			if(ideal)
			{
				if(beforetime != progress <= 0) sauce.PlayDelayed(.05f);
				if(progress >= 1)
				{
					progress = 1;
					start.setui(true);
				}
			}
			else if(progress <= 0)
			{
				progress = 0;
				nextscreen.SetActive(true);
				gameObject.SetActive(false);
			}
			title.localScale = new Vector3(scalex.Evaluate(progress), scaley.Evaluate(progress), 1);
		}
	}

	public void flip()
	{
		if(!ideal || progress < 1) return;
		ideal = false;
		sauce.clip = pancake;
		sauce.PlayDelayed(.1f);
	}
}
