using UnityEngine.UI;
using UnityEngine;

public class bulletroll : MonoBehaviour
{
	Vector2 prevrot;
	[SerializeField] Transform[] t;
	float pan;
	[SerializeField] Texture2D[] d;
	[SerializeField] GameObject goodbullet;
	[SerializeField] Image mouse;
	[SerializeField] Sprite[] mice;

	void Start()
	{
		prevrot = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height, 0);

		for(int y = 0; y < d[0].height; y++)
		for(int x = 0; x < d[0].width; x++)
		{
			d[0].SetPixel(x, y, (y > d[0].height * .75 && x > d[0].width * .6) ? Color.black : Color.white);
			d[2].SetPixel(x, y, new Color(1, .502f, .502f, .502f));
		}
		d[0].Apply();
		d[2].Apply();
	}

	void Update()
	{
		const int sensitivity = 90;
		Vector2 mousepos = new Vector3(Input.mousePosition.x/Screen.width*sensitivity, Input.mousePosition.y/Screen.height*sensitivity, 0);
		if(Input.GetMouseButton(0))
		{
			mouse.sprite = mice[1];
			pan = Mathf.Clamp(pan + prevrot.y - mousepos.y, -90, 90);
			t[0].localEulerAngles = new Vector3(pan, 0, 0);
			t[1].Rotate(0, mousepos.x - prevrot.x, 0, Space.Self);
		}
		else if(Input.GetMouseButton(1))
		{
			mouse.sprite = mice[2];
			if(prevrot != mousepos)
			{
				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
				if(hit.transform.gameObject == goodbullet)
				{
					int bbx = (int)(hit.textureCoord.x * d[0].width - d[1].width * .5f);
					int bby = (int)(hit.textureCoord.y * d[0].height - d[1].height * .5f);
					for(int y = 0; y < d[1].height; y++)
					for(int x = 0; x < d[1].width; x++)
					{
						int currentx = (bbx + x) % d[0].width;
						int currenty = (bby + y) % d[0].height;
						Color currentc = d[1].GetPixel(x, y);
						if(d[0].GetPixel(currentx, currenty).r > currentc.r)
						{
							d[0].SetPixel(currentx, currenty, currentc);
							d[2].SetPixel(currentx, currenty, d[3].GetPixel(x,y));
						}
					}
					d[0].Apply();
					d[2].Apply();
				}
			}
		}
		else mouse.sprite = mice[0];
		prevrot = mousepos;
	}
}
