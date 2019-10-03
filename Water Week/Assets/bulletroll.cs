using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class bulletroll : MonoBehaviour
{
	Vector2 prevrot;
	[SerializeField] Transform[] t;
	float pan;
	Vector4 camrot;
	[SerializeField] Texture2D[] d;
	Texture2D[] dd = new Texture2D[2];
	[SerializeField] GameObject goodbullet;
	[SerializeField] Image mouse;
	[SerializeField] Sprite[] mice;
	[SerializeField] [Range(0.0f, 1.0f)] float influence;
	[SerializeField] Transform tdms;
	[SerializeField] Transform hitthething;
	[SerializeField] ParticleSystem particleseverywhere;
	[SerializeField] Transform tdmsmodel;
	[SerializeField] Material mat;
	Vector3 tdmspos;
	Quaternion tdmsrot;
	bool closenthetip;

	void Start()
	{
		dd[0] = new Texture2D(512, 512, TextureFormat.Alpha8, false, true);
		dd[1] = new Texture2D(512, 512, TextureFormat.RGBA32, false, true);
		clear();
		mat.SetTexture("_c", dd[0]);
		mat.SetTexture("_n", dd[1]);
		prevrot = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height, 0);
		camrot = new Vector4(0, 10, 0, 10);
	}

	void Update()
	{
		const int sensitivity = 120;
		int bbx = -666, bby = -666;
		bool tippp = false;
		int particol = 0;
		mouse.sprite = mice[0];
		Vector2 mousepos = new Vector3(Input.mousePosition.x/Screen.width*sensitivity, Input.mousePosition.y/Screen.height*sensitivity*1.4f, 0);
		if(!EventSystem.current.IsPointerOverGameObject())
		{
			if(prevrot != mousepos)
			{
				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x/Screen.width*200, Input.mousePosition.y/Screen.height*200)), out hit))
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

			if(Input.GetMouseButton(0))
			{
				mouse.sprite = mice[1];
				camrot.x = Mathf.Clamp(camrot.x + prevrot.y - mousepos.y, -90, 90);
				camrot.y =             camrot.y + mousepos.x - prevrot.x;
			}
			if(Input.GetMouseButton(1))
			{
				mouse.sprite = mice[2];
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
				}
				tippp = closenthetip;
			}
		}
		prevrot = mousepos;
		camrot.z = Mathf.Lerp(camrot.z, camrot.x, Time.deltaTime*8);
		camrot.w = Mathf.Lerp(camrot.w, camrot.y, Time.deltaTime*8);
		t[0].localEulerAngles = new Vector3(camrot.z * influence, 0, 0);
		t[1].localEulerAngles = new Vector3(0, (Mathf.Repeat(camrot.w + 180, 360) - 180) * influence, 0);
		tdms.position = Vector3.Lerp(tdms.position, tdmsmodel.position, Time.deltaTime * (tippp ? 20 : 10));
		tdms.rotation = Quaternion.Slerp(tdms.rotation, tdmsmodel.rotation, Time.deltaTime * (tippp ? 10 : 5));
		hitthething.localPosition = hitthething.localPosition * (1 - influence) + new Vector3(0, 0, Mathf.Clamp(hitthething.localPosition.z + Time.deltaTime * (tippp ? -1 : 1), 0, .15f + (1 - influence) * 800));
		var em = particleseverywhere.emission;
		em.rateOverDistanceMultiplier = particol/6f;
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
	}
}
