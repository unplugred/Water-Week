using UnityEngine;
using UnityEngine.UI;

public class loadingscreen : MonoBehaviour
{
	[SerializeField] Text txt;
	[SerializeField] int time;
	[SerializeField] GameObject nextscreen;
	[SerializeField] AudioSource sauce;
	bool played = false;
	float tim;

	void Update()
	{
		float state = Mathf.Clamp01((Mathf.Cos(tim += Time.deltaTime*3)*-.9f) + .3f);
		txt.color = new Color(0,0,0, state);

		if(state > .6)
		{
			if(!played)
			{
				sauce.pitch = Random.Range(.98f,1.02f);
				sauce.panStereo = Random.Range(-.2f,.2f);
				sauce.Play();
				played = true;
			}
		}
		else played = false;

		if(tim >= time*Mathf.PI*2)
		{
			nextscreen.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
