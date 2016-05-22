using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tabo : MonoBehaviour {

	public List<Player> players;
	[SerializeField]float radius;

	public int shotInterval = 500; // ショット間隔 (ms)

	[SerializeField]
	private GameObject shotPrefab;
	private int startTime;
	private int nextShotTime; // 次のショット時間

	private int frontId;
	private int leftId;
	private int rightId;

	[SerializeField]
	private Vector2 vec;
	// 3way 10°ずつ0.5s
	// 方向　弾を打つ bullet 

	private bool shotting = false;


	public void SetTouch(int fid, int lid, int rid)
	{
		this.frontId = fid;
		this.leftId = lid;
		this.rightId = rid;
		try{
			Vector2 fpos = Input.GetTouch(fid).position;
			fpos = Camera.main.ScreenToWorldPoint(fpos);
			Vector2 lpos = Input.GetTouch (lid).position;
			lpos = Camera.main.ScreenToWorldPoint (lpos);
			Vector2 rpos = Input.GetTouch (rid).position;
			rpos = Camera.main.ScreenToWorldPoint (rpos);
			
			Vector2 a = fpos - lpos;
			Vector2 b = fpos - rpos;
			this.vec = (a + b).normalized;
		}catch(Exception e)
		{

		}
		//debug用
		this.vec = new Vector2(4, 3).normalized;


	}

	public void GameStart()
	{
		this.shotting = true;
		this.startTime = GetNow();
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
		this.GameStart ();
		this.SetTouch (-1, -1, -1);
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (this.shotting) {
			if (GetNow() > this.nextShotTime) { 
				// shot
				for (int i = -1; i < 2; i++) {
					GameObject go = GameObject.Instantiate (this.shotPrefab);
					go.GetComponent<Shot> ().Init (this.players, this.Rotate (this.vec, i * 20));
					go.transform.position = Vector3.zero;

				}
				this.nextShotTime += this.shotInterval;
			}
		}
	}

	private Vector2 Rotate(Vector2 vec, int kakudo)
	{
		Vector2 v = new Vector2();
		double d = kakudo * Math.PI / 180;
		v.x = (float)(vec.x * Math.Cos (d) - vec.y * Math.Sin (d));
		v.y = (float)(vec.x * Math.Sin (d) + vec.y * Math.Cos (d));

		Debug.Log ("vec x:" + vec.x + " y:" + vec.y);
		return v;
	}

	public float getRadius()
	{
		return radius;
	}
					
	/// <summary>
	/// get utcnow (ms)
	/// </summary>
	/// <returns>The now.</returns>
	public static int GetNow()
	{
		int i = 0;
		i = (int)(DateTime.UtcNow.Ticks / 10000); // msに変換
		return i;
	}

}
