using UnityEngine;

public class perlpos : MonoBehaviour
{
	[SerializeField]
	float speed = 3;
	[SerializeField]
	float amount = 0.15f;
	Vector2 y;
	Vector3 startpos;

	void Start()
	{
		startpos = transform.localPosition;
		y = new Vector3(
			Random.Range(0f, 100f),
			Random.Range(0f, 100f));
	}

	void Update()
	{
		transform.localPosition = startpos + new Vector3(
			Mathf.PerlinNoise(Time.time * speed, y.x) - 0.5f,
			Mathf.PerlinNoise(Time.time * speed, y.y) - 0.5f, 0) * amount;
	}
}
