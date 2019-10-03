using UnityEngine;
using UnityEngine.UI;

public class mouseinput : MonoBehaviour
{
	public Vector2 mp;
	[SerializeField] Image cursor;
	public bool cursora;

	void Update()
	{
		if(Screen.width > Screen.height)
			mp = new Vector2((Input.mousePosition.x - (Screen.width - Screen.height)*.5f)/Screen.height, Input.mousePosition.y/Screen.height);
			mp = new Vector2(Input.mousePosition.x/Screen.width, (Input.mousePosition.y - (Screen.height - Screen.width)*.5f)/Screen.width);
		Cursor.visible = mp.x < 0 || mp.y < 0 || mp.x > 1 || mp.y > 1;
		cursor.transform.localPosition = new Vector3(mp.x*200 - 101, mp.y*200 - 99);
		cursor.color = new Color(1,1,1,Mathf.Clamp01(cursor.color.a + (cursora ? 10 : -10)*Time.deltaTime));
	}

	public bool checkreigon(float x, float y, float z, float w)
	{
		return !(mp.x < x || mp.y < y || mp.x > z || mp.y > w);
	}
}
