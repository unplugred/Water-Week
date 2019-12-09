using UnityEngine;
using Kino;

public class transition : MonoBehaviour
{
	[SerializeField] AnalogGlitch ag;
	[SerializeField] DigitalGlitch dg;
	[SerializeField] AnimationCurve horizontalshake;
	[SerializeField] AnimationCurve verticaljump;
	[SerializeField] AnimationCurve digitalglitch;
	public static float tim = 5;

	void OnEnable()
	{
		if(tim != 5)
		{
			if(tim >= 2) tim = 0;
		}
		else tim = 6;
	}

	void Update()
	{
		if(tim <= 2)
		{
			tim += Time.deltaTime*3f;
			float val = tim < 1 ? tim : (2 - tim);
			ag.horizontalShake = horizontalshake.Evaluate(val)*.6f;
			ag.verticalJump = verticaljump.Evaluate(val)*.9f;
			dg.intensity = digitalglitch.Evaluate(val)*.95f;
		}
		else enabled = false;
	}
}
