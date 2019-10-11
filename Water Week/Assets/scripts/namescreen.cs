using UnityEngine;
using UnityEngine.UI;

public class namescreen : MonoBehaviour
{
	[SerializeField] Text txtt;
	[SerializeField] Image[] keysss;
	[SerializeField] AnimationCurve keynimation;
	[SerializeField] AnimationCurve fieldanim;
	[SerializeField] Transform field;
	float tim;
	Vector2[] initpos;

	void Start()
	{
		initpos = new Vector2[keysss.Length+1];
		initpos[0] = field.localPosition;
		for(int i = 0; i < keysss.Length; i++)
		{
			initpos[i+1] = keysss[i].transform.localPosition;
		}
	}

	void OnEnable()
	{
		tim = 0;
	}

	void Update()
	{
		tim += Time.deltaTime;
		for(int i = 0; i < keysss.Length; i++)
		{
			keysss[i].transform.localPosition = initpos[i+1] + new Vector2(0, keynimation.Evaluate(tim*1.2f - i*.1f + (i - i%6)*.08f)*140);
		}
		field.localPosition = initpos[0] + new Vector2(0, (1 - fieldanim.Evaluate((tim - .3f)*1.1f))*60);
	}
}
