using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ppl : MonoBehaviour
{
	[SerializeField] GameObject prsn;
	[SerializeField] GameObject prsn2;
	[SerializeField] GameObject treemaster;
	[SerializeField] Sprite[] treesss;
	[SerializeField] Image[] treethingies;
	List<GameObject> pppp = new List<GameObject>();
	const float min = 1;
	const float max = 2;
	const float width = 5;
	int lastsecond;

	void Start()
	{
		float read = -width - Random.Range(0,max);
		while(read < width)
		{
			float much = Random.Range(min,max);
			if(read < 0 && read + much > 0)
				pppp.Add(Instantiate(prsn2, new Vector3(read,-2.88f,-5), Quaternion.Euler(0,90,0), transform));
			else
				pppp.Add(Instantiate(prsn, new Vector3(read,-2.88f,-5), Quaternion.Euler(0,90,0), transform));
			Animator anim = pppp[pppp.Count - 1].GetComponent<Animator>();
			anim.speed = Random.Range(.8f, 1.2f);
			anim.SetFloat("offset",Random.Range(0f,1f));
			read += much;
		}
		foreach(Image img in treethingies)
		{
			img.transform.localPosition = new Vector3(-165 - Random.Range(-565,165),-1,70);
			img.sprite = treesss[Random.Range(0,treesss.Length)];
			img.SetNativeSize();
		}
	}

	void Update()
	{
		foreach(Image img in treethingies)
		{
			if(img.transform.localPosition.x > 165)
			{
				img.transform.localPosition = new Vector3(-165 - Random.Range(0,400),-1,70);
				img.sprite = treesss[Random.Range(0,treesss.Length)];
				img.SetNativeSize();
			}
			else img.transform.Translate(Time.deltaTime*1,0,0,Space.Self);
		}
		if(Mathf.Floor(Time.time) != lastsecond)
		{
			lastsecond = Mathf.FloorToInt(Time.time);
			foreach(Image img in treethingies)
				img.transform.localPosition = new Vector3(Mathf.Round(img.transform.localPosition.x),-1,70);
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}
}
