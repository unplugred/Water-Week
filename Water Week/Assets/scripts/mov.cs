using UnityEngine;

public class mov : MonoBehaviour
{
	[SerializeField] Vector2 movee;
	public bttn button;
	[SerializeField] bool ison;
	float progress = 1;
	Vector2 startpos;

	void Start()
	{
		startpos = transform.localPosition;
	}

	void Update()
	{
		progress = Mathf.Clamp01(progress + Time.deltaTime*(ison ? -2 : 2));
		float amnt = mouseinput.m.uicurve.Evaluate(progress);
		transform.localPosition = startpos + movee*amnt + new Vector2(0, (button != null && button.pressed) ? -1 : 0);
	}

	public void setui(bool ui)
	{
		if(ison != ui)
		{
			ison = ui;
			if(button != null) button.enabled = ui;
		}
	}
}
