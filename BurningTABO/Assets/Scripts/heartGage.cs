using UnityEngine;
using System.Collections;

public class heartGage : MonoBehaviour {

	//0.1秒で1%
	//1.4f上げるのに10秒
	//1秒で0.14f
	float lovePoint;
	const float loveMax = 1.5f;

	// Use this for initialization
	void Start () {
		lovePoint = 0;
	}

	// Update is called once per frame
	void Update () {

	}

	public void addLovePoint(float dt)
	{
		lovePoint += dt * 0.15f;
		lovePoint = Mathf.Min (lovePoint, loveMax);
		this.gameObject.GetComponent<Transform> ().localScale = new Vector3(lovePoint, lovePoint, 1);
	}

	public void setPosition(Vector2 a, Vector2 b)
	{
		this.gameObject.GetComponent<Transform> ().position = new Vector2(Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2)), Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2)));
	}
}
