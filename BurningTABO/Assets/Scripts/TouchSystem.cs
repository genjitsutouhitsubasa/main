﻿using UnityEngine;
using System.Collections;

public class TouchSystem : MonoBehaviour {

    Vector2 cursorPos;
    Vector2 playerPos;

    GameObject[] players;

    int holdFrame;
    int waitFrame;
    float time;


    // Use this for initialization
	void Start () {

        players = GameObject.FindGameObjectsWithTag("Player");

        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (isTouching())
        {
            foreach (Touch t in Input.touches)
            {	
				touchCheckPlayers (t);
            }
        }



		// ------------------------------------------
		//リリース前に消す！！！！！
		// ------------------------------------------
		if (Input.GetMouseButton (0)) {
			Vector2 cursorPos = Input.mousePosition;
			Vector2 worldPos = Camera.main.ScreenToWorldPoint(cursorPos);
			mouseCheckPlayers (worldPos);
		}
		// ------------------------------------------
		//リリース前に消す！！！！！
		// ------------------------------------------


		GameObject inGageAB = GameObject.Find ("HeartInnerGageAB");
		GameObject inGageCD = GameObject.Find ("HeartInnerGageCD");
		GameObject outGageAB = GameObject.Find ("HeartWrapperGageAB");
		GameObject outGageCD = GameObject.Find ("HeartWrapperGageCD");

		//Debug.Log(GameObject.Find("main");// ("HeartInnerGage01"));
		bool isNearAB = false;
		bool isNearCD = false;
		for (int i = 0; i < players.Length; i++) {
			for(int j = i+1; j < players.Length; j++) {
				if (isHitCilcle (players [i].GetComponent<Transform> ().position, players [j].GetComponent<Transform> ().position, players [j].GetComponent<Player> ().getRadius () * 1.5f)) {
					if (players [i].GetComponent<Player>().getGroup() == players [j].GetComponent<Player>().getGroup ()) {
						Debug.Log ("Hit");
						Vector2 a = players [i].GetComponent<Transform> ().position;
						Vector2 b = players [j].GetComponent<Transform> ().position;
						if (players [i].GetComponent<Player> ().getGroup () == 1) {
							inGageAB.GetComponent<SpriteRenderer> ().enabled = true;
							outGageAB.GetComponent<SpriteRenderer> ().enabled = true;
							inGageAB.GetComponent<heartGage> ().addLovePoint (Time.deltaTime);
							inGageAB.transform.position =  ((a + b) / 2);
							outGageAB.transform.position =  ((a + b) / 2);
							isNearAB = true;
						} else {
							inGageCD.GetComponent<SpriteRenderer> ().enabled = true;
							outGageCD.GetComponent<SpriteRenderer> ().enabled = true;
							inGageCD.GetComponent<heartGage> ().addLovePoint (Time.deltaTime);
							inGageCD.transform.position =    ((a + b) / 2);
							outGageCD.transform.position =    ((a + b) / 2);
							isNearCD = true;
						}
					}
				}
			}
		}
		if (!isNearAB) {
			inGageAB.GetComponent<SpriteRenderer> ().enabled = false;
			outGageAB.GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (!isNearCD) {
			inGageCD.GetComponent<SpriteRenderer> ().enabled = false;
			outGageCD.GetComponent<SpriteRenderer> ().enabled = false;
		}
			
    }

	void mouseCheckPlayers(Vector2 mousePos)
	{
		foreach (GameObject player in players) {
			if (isHitCilcle (mousePos, player.transform.position, player.GetComponent<Player> ().getRadius())) {
				player.GetComponent<Player> ().Move (mousePos);
				player.GetComponent<Player> ().setTouch (1);
				break;
			}
		}
	}

	void touchCheckPlayers(Touch t)
	{
		switch(t.phase)
		{
		case TouchPhase.Canceled:
		case TouchPhase.Ended:
			foreach (GameObject player in players) {
				if (t.fingerId == player.GetComponent<Player> ().getTouch ()) {
					player.GetComponent<Player> ().setTouch (-1);
				}
			}
			break;
		case TouchPhase.Stationary:
		case TouchPhase.Moved:
			foreach (GameObject player in players) {
				if (t.fingerId == player.GetComponent<Player> ().getTouch ()) {
					player.GetComponent<Player> ().Move (Camera.main.ScreenToWorldPoint(t.position));
					break;
				}
			}
			break;
		case TouchPhase.Began:
			foreach (GameObject player in players) {
				if (isHitTap (Camera.main.ScreenToWorldPoint(t.position), player.transform.position, player.GetComponent<Player> ().getRadius())) {
					player.GetComponent<Player> ().Move (Camera.main.ScreenToWorldPoint(t.position));
					player.GetComponent<Player> ().setTouch (t.fingerId);
					break;
				}
			}
			break;
		}
	}

    bool isTouching()
    {
        return Input.GetMouseButton(0) || Input.touchCount > 0;
    }

    bool isHitTap(Vector2 tapPoint, Vector2 targetPoint, float targetSize)
    {
        if (Mathf.Pow(tapPoint.x - targetPoint.x, 2) +
            Mathf.Pow(tapPoint.y - targetPoint.y, 2)
            < targetSize)
            return true;
        return false;
    }

	bool isHitCilcle(Vector2 a, Vector2 b, float radius)
	{	
		if (Mathf.Pow (a.x - b.x, 2) +
		   Mathf.Pow (a.y - b.y, 2)
		   <= Mathf.Pow (2 * radius, 2))
			return true;
		return false;
	}
}