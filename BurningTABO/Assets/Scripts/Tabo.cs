using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tabo : MonoBehaviour {

	public List<Player> players;

	public long shotInterval = 500; // ショット間隔

	[SerializeField]
	private GameObject shotPrefab;
	private long startTime;
	private long nextShotTime; // 次のショット時間

	private int frontId;
	private int leftId;
	private int rightId;

	private Vector2 vec;
	// 3way 10°ずつ0.5s
	// 方向　弾を打つ bullet 

	private bool shotting = false;


	public void SetTouch(int fid, int lid, int rid)
	{
		this.frontId = fid;
		this.leftId = lid;
		this.rightId = rid;

		Vector2 fpos = Input.GetTouch(fid).position;
		fpos = Camera.main.ScreenToWorldPoint(fpos);
		Vector2 lpos = Input.GetTouch (lid).position;
		lpos = Camera.main.ScreenToWorldPoint (lpos);
		Vector2 rpos = Input.GetTouch (rid).position;
		rpos = Camera.main.ScreenToWorldPoint (rpos);

		Vector2 a = fpos - lpos;
		Vector2 b = fpos - rpos;
		this.vec = (a + b).normalized;


	}

	public void GameStart()
	{
		this.shotting = true;
		this.startTime = DateTime.UtcNow.Ticks;
		this.nextShotTime = this.startTime + this.shotInterval;
	}

	public void GameEnd()
	{
		this.shotting = false;
	}

	private void Awake()
	{
		this.frontId = this.leftId = this.rightId = -1;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (this.shotting) {
			if (DateTime.UtcNow.Ticks > this.nextShotTime) {
				// shot
				for (int i = -1; i < 2; i++) {
					GameObject go = GameObject.Instantiate (this.shotPrefab);
					go.GetComponent<Shot> ().Init (this.players, this.Rotate (this.vec, i * 10));
				}
			}
		}
	}

	private Vector2 Rotate(Vector2 vec, int kakudo)
	{
		Vector2 v = new Vector2();
		float f = kakudo * 180 / Math.PI;
		v.x = vec.x * (float)Math.Cos (f) - vec.y * (float)Math.Sin (f);
		v.y = vec.x * (float)Math.Sin (f) + vec.y * (float)Math.Cos (f);
		return v;
	}
					
}
