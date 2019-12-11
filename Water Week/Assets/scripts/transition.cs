using UnityEngine;
using Kino;

public class transition : MonoBehaviour
{
	[SerializeField] AnalogGlitch ag;
	[SerializeField] DigitalGlitch dg;
	[SerializeField] AnimationCurve horizontalshake;
	[SerializeField] AnimationCurve verticaljump;
	[SerializeField] AnimationCurve digitalglitch;
	float adtime;
	[SerializeField] AudioSource sauce;
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
			adtime -= Time.deltaTime;
			if(adtime < 0)
			{
				adtime = Random.Range(.0f,.1f);
				sauce.mute = !sauce.mute;
				sauce.pitch = Random.Range(.8f,1.2f);
				sauce.panStereo = Random.Range(-.3f,.3f);
			}
		}
		else
		{
			sauce.mute = true;
			enabled = false;
		}
	}
}
