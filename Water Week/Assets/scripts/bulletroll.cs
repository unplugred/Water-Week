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
	[SerializeField] Image[] uis;
	[SerializeField] Sprite[] sprites;
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
	public bool ocui = false;
	float ocuim = 1;
	bool ocstart = false;
	float ocmstart = 1;
	[SerializeField] AnimationCurve uicruve;
	Vector2[] startpos = new Vector2[3];

	void Start()
	{
		dd[0] = new Texture2D(512, 512, TextureFormat.Alpha8, false, true);
		dd[1] = new Texture2D(512, 512, TextureFormat.RGBA32, false, true);
		clear();
		mat.SetTexture("_c", dd[0]);
		mat.SetTexture("_n", dd[1]);
		startpos[0] = uis[0].transform.localPosition;
		startpos[1] = uis[1].transform.localPosition;
		startpos[2] = uis[2].transform.localPosition;
	}

	void Update()
	{
		const int sensitivity = 120;
		int bbx = -666, bby = -666;
		bool tippp = false;
		if(influence == 1)
		{
			int particol = 0;
			uis[0].sprite = sprites[0];
			Vector2 mousepos = new Vector2(mp.mp.x*sensitivity, mp.mp.y*sensitivity*1.4f);
			if(mp.checkreigon(.035f, .035f, .24f, .155f))
			{
				uis[2].sprite = sprites[6];
				if(Input.GetMouseButtonUp(0))
				{
					timeline.Play();
					mp.cursora = false;
					ocui = false;
					nametag.gameObject.SetActive(false);
				}
				if(Input.GetMouseButton(0)) uis[1].sprite = sprites[5];
				else uis[1].sprite = sprites[4];
			}
			else if(mp.checkreigon(.255f, .03f, .36f, .16f))
			{
				uis[1].sprite = sprites[3];
				if(Input.GetMouseButtonUp(0)) clear();
				if(Input.GetMouseButton(0)) uis[2].sprite = sprites[8];
				else uis[2].sprite = sprites[7];
			}
			else
			{
				uis[1].sprite = sprites[3];
				uis[2].sprite = sprites[6];
				if(prevrot != mousepos)
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

				if(Input.GetMouseButton(1))
				{
					uis[0].sprite = sprites[2];
					camrot.x = Mathf.Clamp(camrot.x + prevrot.y - mousepos.y, -90, 90);
					camrot.y =             camrot.y + mousepos.x - prevrot.x;
				}
				if(Input.GetMouseButton(0))
				{
					uis[0].sprite = sprites[1];
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
						ocstart = true;
					}
					tippp = closenthetip;
				}
			}
			prevrot = mousepos;
			var em = particleseverywhere.emission;
			em.rateOverDistanceMultiplier = particol/6f;
		}
		ocuim = Mathf.Clamp01(ocuim + Time.deltaTime*(ocui ? -2 : 2));
		float amnt = uicruve.Evaluate(ocuim);
		uis[0].transform.localPosition = startpos[0] + new Vector2(0, amnt*44);
		uis[2].transform.localPosition = startpos[2] + new Vector2(0, amnt*-33);
		ocmstart = Mathf.Clamp01(ocmstart + Time.deltaTime*(ocstart ? -2 : 2));
		uis[1].transform.localPosition = startpos[1] + new Vector2(0, uicruve.Evaluate(ocmstart)*-33);
		tdms.position = Vector3.Lerp(tdms.position, tdmsmodel.position, Time.deltaTime * (tippp ? 20 : 10));
		tdms.rotation = Quaternion.Slerp(tdms.rotation, tdmsmodel.rotation, Time.deltaTime * (tippp ? 10 : 5));
		camrot.z = Mathf.Lerp(camrot.z, camrot.x, Time.deltaTime*8);
		camrot.w = Mathf.Lerp(camrot.w, camrot.y, Time.deltaTime*8);
		t[0].localEulerAngles = new Vector3(camrot.z * influence, 0, 0);
		t[1].localEulerAngles = new Vector3(0, (Mathf.Repeat(camrot.w + 180, 360) - 180) * influence, 0);
		hitthething.localPosition = hitthething.localPosition * (1 - influence) + new Vector3(0, 0, Mathf.Clamp(hitthething.localPosition.z + Time.deltaTime * (tippp ? -1 : 1), 0, .15f + (1 - influence) * 800));
	}

	public void clear()
	{
		Color n = d[1].GetPixel(0,0);
		for(int y = 0; y < dd[0].height; y++)
		for(int x = 0; x < dd[0].width; x++)
		{
			dd[0].SetPixel(x, y, (y > dd[0].height * .75 && x > dd[0].width * .6) ? Color.clear : Color.black);
			dd[1].SetPixel(x, y, n);
		}
		dd[0].Apply();
		dd[1].Apply();
		ocstart = false;
	}
}
