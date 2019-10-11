using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class bttn : MonoBehaviour
{
	[SerializeField] Image img;
	[SerializeField] Sprite[] spr;
	[SerializeField] Vector4 pos;
	[SerializeField] Vector2 mov;
	[SerializeField] UnityEvent onclick;
	[SerializeField] Transform text;
	public bool ison;
	float progress = 1;
	Vector2 startpos;
	Vector2 textpos;

	void Start()
	{
		startpos = transform.localPosition;
		if(text != null) textpos = text.localPosition;
	}

	void Update()
	{
		if(ison && mouseinput.m.checkreigon(pos))
		{
			if(Input.GetMouseButton(0))
			{
				if(img.sprite != spr[2])
				{
					if(text != null) text.localPosition = textpos + new Vector2(0, -1);
					img.sprite = spr[2];
				}
			}
			else
			{
				if(Input.GetMouseButtonUp(0)) onclick.Invoke();
				if(img.sprite != spr[1])
				{
					if(text != null) text.localPosition = textpos;
					img.sprite = spr[1];
				}
			}
		}
		else
		{
			if(img.sprite != spr[0])
			{
				if(text != null) text.localPosition = textpos;
				img.sprite = spr[0];
			}
		}
		if(mov != Vector2.zero)
		{
			progress = Mathf.Clamp01(progress + Time.deltaTime*(ison ? -2 : 2));
			float amnt = mouseinput.m.uicurve.Evaluate(progress);
			transform.localPosition = startpos + mov*amnt;
		}
	}

	public void setui(bool ui)
	{
		ison = ui;
	}
}
