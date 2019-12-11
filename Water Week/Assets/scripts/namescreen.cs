﻿using UnityEngine;
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
	[SerializeField] Text finaltext;
	[SerializeField] AudioSource sauce;
	[SerializeField] AudioClip[] pancake;
	float tim;
	Vector2[] initpos;
	int inty;
	float notim = 1;
	bool onoff = true;
	char[] keypadthing = {'\b','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',' ','\n'};
	KeyCode[] keymapping = {KeyCode.Backspace,KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,KeyCode.H,KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,KeyCode.U,KeyCode.V,KeyCode.W,KeyCode.X,KeyCode.Y,KeyCode.Z,KeyCode.Space,KeyCode.Return};

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
		bool keyboard = false;
		int input = -1;

		//mouse input
		if(mouseinput.m.checkreigon(.2f,.2f,.8f,.7f))
			input = Mathf.Min((int)((mouseinput.m.mp.x - .2f)*10) + ((int)((.7f - mouseinput.m.mp.y)*10))*6, 28);
		//keyboard input
		if((!Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0)) || input < 0)
			for(int i = 0; i < keypadthing.Length; i++)
			{
				if(Input.GetKey(keymapping[i]))
				{
					input = i;
					keyboard = true;
					keysss[i].sprite = keypresses[2];
				}
				if(Input.GetKeyUp(keymapping[i]))
				{
					handlechar(keypadthing[i]);
					keysss[i].sprite = keypresses[0];
				}
			}
		if(input != inty)
		{
			if(inty  >= 0) keysss[inty ].sprite = keypresses[0];
			if(input >= 0 && !keyboard) keysss[input].sprite = keypresses[1];
			inty = input;
		}
		if(Input.GetMouseButton(0) && input >= 0) keysss[input].sprite = keypresses[2];
		if(Input.GetMouseButtonUp(0) && input >= 0)
		{
			keysss[input].sprite = keypresses[1];
			handlechar(keypadthing[input]);
		};

		//animations
		if(tim == 0) transform.localPosition = Vector2.zero;
		tim = Mathf.Clamp(tim + Time.deltaTime * (onoff ? 1 : -1), 0, 1.5f);
		notim += Time.deltaTime*1.5f;
		for(int i = 0; i < keysss.Length; i++)
			keysss[i].transform.localPosition = initpos[i+1] + new Vector2(0, keynimation.Evaluate(tim*1.2f - i*.1f + (i - i%6)*.08f)*140 - ((input == i && (Input.GetMouseButton(0) || keyboard)) ? 1 : 0));
		field.localPosition = initpos[0] + new Vector2(nono.Evaluate(notim)*5, (1 - fieldanim.Evaluate((tim - .3f)*1.1f))*60);
		if(tim == 0 && !onoff)
		{
			actualgame.SetActive(true);
			gameObject.SetActive(false);
		}
	}

	void handlechar(char ch)
	{
		switch(ch)
		{
			//delete letter
			case '\b':
				if(txtt.text != "")
				{
					txtt.text = txtt.text.Substring(0, txtt.text.Length - 1);
					sauce.pitch = Random.Range(.73f, 0.77f);
					sauce.panStereo = Random.Range(-.2f,.2f);
					sauce.clip = pancake[0];
					sauce.Play();
				}
				else angry();
				break;
			//next screen
			case '\n':
				if(txtt.text.Length > 0)
				{
					if(txtt.text.EndsWith(" ")) txtt.text = txtt.text.Substring(0,txtt.text.Length - 1);
					onoff = false;
					finaltext.text = txtt.text.ToUpper() + "'s bullet";
					sauce.pitch = 1;
					sauce.panStereo = 0;
					sauce.clip = pancake[2];
					sauce.Play();
				}
				else angry();
				break;
			//space
			case ' ':
				if(txtt.text.Length < 11)
				{
					if(txtt.text != "" && !txtt.text.EndsWith(" ")) txtt.text += " ";
					sauce.pitch = Random.Range(.98f, 1.02f);
					sauce.panStereo = Random.Range(-.2f,.2f);
					sauce.clip = pancake[0];
					sauce.Play();
				}
				else angry();
				break;
			//any letter
			default:
				if(txtt.text.Length < 11)
				{
					txtt.text += ch;
					sauce.pitch = Random.Range(.98f, 1.02f);
					sauce.panStereo = Random.Range(-.2f,.2f);
					sauce.clip = pancake[0];
					sauce.Play();
				}
				else angry();
				break;
		}
	}

	void angry()
	{
		if(notim < 1) return;
		notim = 0;
		sauce.pitch = Random.Range(.98f, 1.02f);
		sauce.panStereo = Random.Range(-.2f,.2f);
		sauce.clip = pancake[1];
		sauce.Play();
	}
}
