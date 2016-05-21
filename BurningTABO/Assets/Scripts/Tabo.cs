using UnityEngine;
using System.Collections;

public class Tabo : MonoBehaviour {

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
			// シューティング処理

		}
	}
}
