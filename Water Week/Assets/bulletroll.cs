using UnityEngine.UI;
using UnityEngine;

public class bulletroll : MonoBehaviour
{
	Vector2 prevrot;
	[SerializeField] Transform[] t;
	float pan;
	Vector4 camrot;
	[SerializeField] Texture2D[] d;
	[SerializeField] GameObject goodbullet;
	[SerializeField] Image mouse;
	[SerializeField] Sprite[] mice;
	[SerializeField] [Range(0.0f, 1.0f)] float influence;
	[SerializeField] Transform tdms;
	[SerializeField] Transform hitthething;

	void Start()
	{
		prevrot = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height, 0);
		camrot = new Vector4(0, 170, 0, 170);

		for(int y = 0; y < d[0].height; y++)
		for(int x = 0; x < d[0].width; x++)
		{
			d[0].SetPixel(x, y, (y > d[0].height * .75 && x > d[0].width * .6) ? Color.clear : Color.black);
			d[2].SetPixel(x, y, new Color(1, .502f, .502f, .502f));
		}
		d[0].Apply();
		d[2].Apply();
	}

	void Update()
	{
		const int sensitivity = 120;
		Vector2 mousepos = new Vector3(Input.mousePosition.x/Screen.width*sensitivity, Input.mousePosition.y/Screen.height*sensitivity*1.4f, 0);
		int bbx = -666, bby = -666;
		if(prevrot != mousepos)
		{
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				if(hit.transform.gameObject == goodbullet && (hit.textureCoord.x < .6f || hit.textureCoord.y < .75f))
				{
					bbx = (int)(hit.textureCoord.x * d[0].width  - d[1].width  * .5f);
					bby = (int)(hit.textureCoord.y * d[0].height - d[1].height * .5f);
					tdms.localPosition = hit.point;
					tdms.rotation = Quaternion.LookRotation(hit.normal);
				}
				else tdms.localPosition = new Vector3(666, 666, 666);
			}
			else tdms.localPosition = new Vector3(666, 666, 666);
		}

		if(Input.GetMouseButton(0))
		{
			mouse.sprite = mice[1];
			camrot.x = Mathf.Clamp(camrot.x + prevrot.y - mousepos.y, -90, 90);
			camrot.y =             camrot.y + mousepos.x - prevrot.x;
		}
		else if(Input.GetMouseButton(1))
		{
			mouse.sprite = mice[2];
			if(bbx != -666)
			{
				for(int y = 0; y < d[1].height; y++)
				for(int x = 0; x < d[1].width; x++)
				{
					int currentx = (bbx + x) % d[0].width;
					int currenty = (bby + y) % d[0].height;
					Color currentc = d[1].GetPixel(x, y);
					if(d[0].GetPixel(currentx, currenty).a > currentc.a)
					{
						d[0].SetPixel(currentx, currenty, currentc);
						d[2].SetPixel(currentx, currenty, d[3].GetPixel(x,y));
					}
				}
				d[0].Apply();
				d[2].Apply();
			}
		}
		else mouse.sprite = mice[0];
		prevrot = mousepos;
		camrot.z = Mathf.Lerp(camrot.z, camrot.x, Time.deltaTime*8);
		camrot.w = Mathf.Lerp(camrot.w, camrot.y, Time.deltaTime*8);
		t[0].localEulerAngles = new Vector3(camrot.z * influence, 0, 0);
		t[1].localEulerAngles = new Vector3(0, (Mathf.Repeat(camrot.w, 360) - 180) * influence + 180, 0);
	}
}
