using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Playables;

public class bulletroll : MonoBehaviour
{
	Vector2 prevrot = new Vector2(0,0);
	[SerializeField] Transform[] t;
	float pan;
	Vector4 camrot = new Vector4(0,10,0,10);
	[SerializeField] Texture2D[] d;
	Texture2D[] dd = new Texture2D[2];
	[SerializeField] GameObject goodbullet;
	[SerializeField] Image mouseview;
	[SerializeField] Sprite[] mousesprites;
	[SerializeField] mov[] uis;
	[Range(0.0f, 1.0f)] public float influence;
	[SerializeField] Transform tdms;
	[SerializeField] Transform hitthething;
	[SerializeField] ParticleSystem particleseverywhere;
	[SerializeField] Transform tdmsmodel;
	[SerializeField] Material mat;
	[SerializeField] mouseinput mp;
	[SerializeField] PlayableDirector timeline;
	[SerializeField] Text nametag;
	Vector3 tdmspos;
	Quaternion tdmsrot;
	bool closenthetip;
	[SerializeField] AnimationCurve shakecurve;
	float shakeprogress = 1;
	int overolparticol;
	[SerializeField] ParticleSystem shakeyshakey;
	[SerializeField] Transform gameui;
	[SerializeField] AnimationCurve camcurve;
	float camprogress = 0;
	[SerializeField] AudioSource clipclap;
	[SerializeField] AudioClip[] clips;
	float soundtimer;

	void Start()
	{
		dd[0] = new Texture2D(512, 512, TextureFormat.Alpha8, false, true);
		dd[1] = new Texture2D(512, 512, TextureFormat.RGBA32, false, true);
		clear();
		mat.SetTexture("_c", dd[0]);
		mat.SetTexture("_n", dd[1]);
		gameui.gameObject.SetActive(true);
	}

	void Update()
	{
		const int sensitivity = 120;
		int bbx = -666, bby = -666;
		bool tippp = false;
		float shake = 0;
		bool isui = false;
		if(influence == 1)
		{
			isui = true;
			int particol = 0;
			mouseview.sprite = mousesprites[0];
			Vector2 mousepos = new Vector2(mp.mp.x*sensitivity, mp.mp.y*sensitivity*1.4f);

			//raycast drawing and tip
			if(prevrot != mousepos || Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ScreenPointToRay(mp.mp*200), out hit))
				{
					if(hit.transform.gameObject == goodbullet && (hit.textureCoord.x < .6f || hit.textureCoord.y < .75f))
					{
						bbx = (int)(hit.textureCoord.x * dd[0].width  - d[0].width *.5f);
						bby = (int)(hit.textureCoord.y * dd[0].height - d[0].height*.5f);
						tdmsmodel.position = hit.point;
						tdmsmodel.rotation = Quaternion.LookRotation(hit.normal);
						closenthetip = true;
					}
					else closenthetip = false;
				}
				else closenthetip = false;
			}
			mp.cursora = !closenthetip;

			//rotate camera
			if(Input.GetMouseButton(1))
			{
				mouseview.sprite = mousesprites[2];
				camrot.x = Mathf.Clamp(camrot.x + prevrot.y - mousepos.y, -90, 90);
				camrot.y =             camrot.y + mousepos.x - prevrot.x;
			}

			//draw onto texture
			if(Input.GetMouseButton(0) && shakeprogress == 1 && camprogress == 1)
			{
				if(!uis[1].button.pressed && !uis[2].button.pressed) mouseview.sprite = mousesprites[1];
				if(bbx != -666)
				{
					for(int y = 0; y < d[0].height; y++)
					for(int x = 0; x < d[0].width ; x++)
					{
						int currentx = (bbx + x) % dd[0].width;
						int currenty = (bby + y) % dd[0].height;
						Color currentc = d[0].GetPixel(x, y);
						if(dd[0].GetPixel(currentx, currenty).a > currentc.a)
						{
							particol++;
							dd[0].SetPixel(currentx, currenty, currentc);
							dd[1].SetPixel(currentx, currenty, d[1].GetPixel(x,y));
						}
					}
					dd[0].Apply();
					dd[1].Apply();
					uis[2].setui(true);
				}
				tippp = closenthetip;
			}

			//shake to delete
			if(shakeprogress < 1)
			{
				if((shakeprogress += Time.deltaTime*1.3f) > 1)
				{
					shakeprogress = 1;
					clear();
					mat.SetFloat("_s", 1);
				}
				else
				{
					mat.SetFloat("_s", 1 - shakeprogress);
				}
				shake = shakecurve.Evaluate(shakeprogress);
			}

			prevrot = mousepos;

			//drawing particles
			var em = particleseverywhere.emission;
			em.rateOverDistanceMultiplier = particol*.1f;
			overolparticol += particol;

			soundtimer -= Time.deltaTime;
			if(particol > 100 && soundtimer < 0)
			{
				soundtimer = Random.Range(0.02f,.1f);
				clipclap.panStereo = Random.Range(-.1f,.1f);
				clipclap.pitch = Random.Range(.8f,1.2f);
				clipclap.PlayOneShot(clips[Random.Range(0,clips.Length)], Random.Range(.6f,1f));
			}

			//camera start animation
			if(camprogress < 1)
			{
				if(camprogress == 0) gameui.localPosition = Vector2.zero;
				camprogress = Mathf.Min(camprogress += Time.deltaTime*.5f, 1);
				Camera.main.transform.position = new Vector3(0,-1.9f*(1 - camcurve.Evaluate(camprogress)),2);
				isui = false;
			}
		}

		nametag.color = new Color(0,0,0,Mathf.Clamp(nametag.color.a + Time.deltaTime*(isui ? 5 : -5),0,.95f));
		tdms.position = Vector3.Lerp(tdms.position, tdmsmodel.position, Time.deltaTime*(tippp ? 20 : 10));
		tdms.rotation = Quaternion.Slerp(tdms.rotation, tdmsmodel.rotation, Time.deltaTime*(tippp ? 10 : 5));
		camrot.z = Mathf.Lerp(camrot.z, camrot.x, Time.deltaTime*8);
		camrot.w = Mathf.Lerp(camrot.w, camrot.y, Time.deltaTime*8);
		t[0].localEulerAngles = new Vector3(camrot.z * influence + Mathf.Sin(Time.time*20)*shake, 0, 0);
		t[1].localEulerAngles = new Vector3(0, (Mathf.Repeat(camrot.w + 180, 360) - 180)*influence + Mathf.Cos(Time.time*20)*shake, 0);
		hitthething.localPosition = hitthething.localPosition*(1 - influence) + new Vector3(0, 0, Mathf.Clamp(hitthething.localPosition.z + Time.deltaTime*(tippp ? -1 : 1), 0, .15f + (1 - influence)*800));
		uis[0].setui(isui);
		uis[1].setui(isui);
	}

	public void clear()
	{
		Color n = d[1].GetPixel(0,0);
		for(int y = 0; y < dd[0].height; y++)
		for(int x = 0; x < dd[0].width; x++)
		{
			dd[0].SetPixel(x, y, Color.black);
			dd[1].SetPixel(x, y, n);
		}
		dd[0].Apply();
		dd[1].Apply();
	}

	public void trash()
	{
		if(shakeprogress > .5) shakeprogress = 1 - Mathf.Min(shakeprogress, 1);
		if(shakeprogress == 0)
		{
			shakeyshakey.Play();
			var emm = shakeyshakey.emission;
			emm.rateOverTimeMultiplier = overolparticol*.0025f;
			overolparticol = 0;
		}
		uis[2].setui(false);
	}

	public void shoot()
	{
		timeline.Play();
		uis[2].setui(false);
		mp.cursora = false;
	}
}
