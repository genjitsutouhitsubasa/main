using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shot : MonoBehaviour {
	[SerializeField]
	private float radius; // 円判定の半径

	public Vector2 vec; // 飛ぶ方向
	public float speed;

	public List<Player> players = new List<Player>();

	public void Init(List<Player> p, Vector2 vec)
	{
		this.players = p;
		this.vec = vec;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 
		this.transform.position += new Vector3((vec * speed).x, (vec * speed).y, 0);

		// 当たり判定

	}
}
