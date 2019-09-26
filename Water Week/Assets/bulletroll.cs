using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletroll : MonoBehaviour
{
	Vector2 prevrot;
	[SerializeField] Transform[] t;
	float pan;
	[SerializeField] Texture2D bulletmap;
	[SerializeField] Texture2D bullethole;
	[SerializeField] GameObject goodbullet;

	void Start()
	{
		prevrot = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height, 0);

		for(int y = 0; y < bulletmap.height; y++)
		for(int x = 0; x < bulletmap.width; x++)
			bulletmap.SetPixel(x, y, (y > bulletmap.height * .75 && x > bulletmap.width * .6) ? Color.black : Color.white);
		bulletmap.Apply();
	}

	void Update()
	{
		const int sensitivity = 90;
		Vector2 mousepos = new Vector3(Input.mousePosition.x/Screen.width*sensitivity, Input.mousePosition.y/Screen.height*sensitivity, 0);
		if(Input.GetMouseButton(0))
		{
			pan = Mathf.Clamp(pan + prevrot.y - mousepos.y, -90, 90);
			t[0].localEulerAngles = new Vector3(pan, 0, 0);
			t[1].Rotate(0, mousepos.x - prevrot.x, 0, Space.Self);
		}
		else if(Input.GetMouseButton(1))
		{
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			if(hit.transform.gameObject == goodbullet)
			{
				int bbx = (int)(hit.textureCoord.x * bulletmap.width - bullethole.width * .5f);
				int bby = (int)(hit.textureCoord.y * bulletmap.height - bullethole.height * .5f);
				for(int y = 0; y < bullethole.height; y++)
				for(int x = 0; x < bullethole.width; x++)
				{
					int currentx = (bbx + x) % bulletmap.width;
					int currenty = (bby + y) % bulletmap.height;
					Color currentc = bullethole.GetPixel(x, y);
					if(bulletmap.GetPixel(currentx, currenty).r > currentc.r)
						bulletmap.SetPixel(currentx, currenty, currentc);
				}
				bulletmap.Apply();
			}
		}
		prevrot = mousepos;
	}
}
