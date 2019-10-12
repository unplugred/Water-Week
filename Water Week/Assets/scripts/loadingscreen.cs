using UnityEngine;
using UnityEngine.UI;

public class loadingscreen : MonoBehaviour
{
	[SerializeField] Text txt;
	[SerializeField] int time;
	[SerializeField] GameObject nextscreen;
	float tim;

	void Update()
	{
		txt.color = new Color(0,0,0, Mathf.Clamp01((Mathf.Cos(tim += Time.deltaTime*3)*-.9f) + .3f));
		if(tim >= time*Mathf.PI*2)
		{
			nextscreen.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
