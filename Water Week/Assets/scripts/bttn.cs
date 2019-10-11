using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class bttn : MonoBehaviour
{
	[SerializeField] Image img;
	[SerializeField] Sprite[] spr;
	[SerializeField] UnityEvent onclick;
	[SerializeField] Vector4 pos;
	public bool pressed;
	public bool hover;

	void Update()
	{
		if(mouseinput.m.checkreigon(pos))
		{
			if(Input.GetMouseButton(0))
			{
				if(img.sprite != spr[2])
				{
					pressed = true;
					hover = true;
					img.sprite = spr[2];
				}
			}
			else
			{
				if(Input.GetMouseButtonUp(0)) onclick.Invoke();
				if(img.sprite != spr[1])
				{
					pressed = false;
					hover = true;
					img.sprite = spr[1];
				}
			}
		}
		else if(img.sprite != spr[0])
		{
			pressed = false;
			hover = false;
			img.sprite = spr[0];
		}
	}
}
