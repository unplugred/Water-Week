using UnityEngine;
using UnityEngine.UI;

public class namescreen : MonoBehaviour
{
	[SerializeField] Text txtt;
	[SerializeField] Image[] keysss;
	[SerializeField] AnimationCurve keynimation;
	[SerializeField] AnimationCurve fieldanim;
	[SerializeField] Transform field;
	[SerializeField] Sprite[] keypresses;
	[SerializeField] AnimationCurve nono;
	[SerializeField] GameObject actualgame;
	float tim;
	Vector2[] initpos;
	int inty;
	float notim = 1;
	bool onoff = true;
	char[] keypadthing = {'.','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',' ','*','*'};

	void Start()
	{
		initpos = new Vector2[keysss.Length+1];
		initpos[0] = field.localPosition;
		for(int i = 0; i < keysss.Length; i++)
			initpos[i+1] = keysss[i].transform.localPosition;
	}

	void OnEnable()
	{
		tim = 0;
	}

	void Update()
	{
		int input = -1;
		if(mouseinput.m.checkreigon(.2f,.2f,.8f,.7f))
			input = Mathf.Min((int)((mouseinput.m.mp.x - .2f)*10) + ((int)((.7f - mouseinput.m.mp.y)*10))*6, 28);
		if(input != inty)
		{
			if(inty  >= 0) keysss[inty ].sprite = keypresses[0];
			if(input >= 0) keysss[input].sprite = keypresses[1];
			inty = input;
		}
		if(Input.GetMouseButton(0) && input >= 0) keysss[input].sprite = keypresses[2];
		if(Input.GetMouseButtonUp(0) && input >= 0)
		{
			keysss[input].sprite = keypresses[1];
			switch(keypadthing[input])
			{
				case '.':
					if(txtt.text == "")
					{
						if(notim >= 1) notim = 0;
					}
					else txtt.text = txtt.text.Substring(0, txtt.text.Length - 1);
					break;
				case '*':
					if(txtt.text.Length > 0)
					{
						if(txtt.text.EndsWith(" ")) txtt.text = txtt.text.Substring(0,txtt.text.Length - 1);
						onoff = false;
					}
					break;
				case ' ':
					if(txtt.text.Length >= 11)
					{
						if(notim >= 1) notim = 0;
					}
					else if(txtt.text != "" && !txtt.text.EndsWith(" ")) txtt.text += " ";
					break;
				default:
					if(txtt.text.Length >= 11)
					{
						if(notim >= 1) notim = 0;
					}
					else txtt.text += keypadthing[input];
					break;
			}
		}

		if(tim == 0) transform.localPosition = Vector2.zero;
		tim = Mathf.Clamp(tim + Time.deltaTime * (onoff ? 1 : -1), 0, 1.5f);
		notim += Time.deltaTime*1.5f;
		for(int i = 0; i < keysss.Length; i++)
			keysss[i].transform.localPosition = initpos[i+1] + new Vector2(0, keynimation.Evaluate(tim*1.2f - i*.1f + (i - i%6)*.08f)*140 - ((input == i && Input.GetMouseButton(0)) ? 1 : 0));
		field.localPosition = initpos[0] + new Vector2(nono.Evaluate(notim)*5, (1 - fieldanim.Evaluate((tim - .3f)*1.1f))*60);
		if(tim == 0 && !onoff)
		{
			actualgame.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
