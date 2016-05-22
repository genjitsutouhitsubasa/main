using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Shot : MonoBehaviour {
	[SerializeField]
	private float radius; // 円判定の半径

	public Vector2 vec; // 飛ぶ方向
	public float speed;

	public List<Player> players = new List<Player>();

	private int startTime;

	public void Init(List<Player> p, Vector2 vec)
	{
		this.players = p;
		this.vec = vec;
		this.speed = 0.5f;
		this.startTime = Tabo.GetNow();
		this.radius = 0.3f;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 
		// this.transform.position += new Vector3((vec * speed).x, (vec * speed).y, 0);
		this.transform.position = this.transform.position + new Vector3 (vec.x * speed, vec.y * speed, 0);

		// 当たり判定

		foreach (Player p in this.players) {
			if ((this.transform.position - p.transform.position).magnitude > this.radius + p.getRadius ()) {
				// not hit
			} else {
				// hit
				p.SendMessage("Hit", this.transform);
			}
		}


		// 5000ms で消える
		if (Tabo.GetNow() > startTime + 5000) {
			GameObject.Destroy (this.gameObject);
		}

	}
}
