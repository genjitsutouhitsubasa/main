using UnityEngine;
using System.Collections;

public class TouchSystem : MonoBehaviour {

    Vector2 cursorPos;
    Vector2 playerPos;

    GameObject[] players;

    int holdFrame;
    int waitFrame;
    float time;


    enum Mode
    {
        NOTHING,
        TOUCH,
        SWIPE,
        SEPARATE,
        END
    };
    Mode nowMode = Mode.NOTHING;

    // Use this for initialization
    void Start () {

        players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players)
            Debug.Log(player);

        time = 0;
    }

    void Update()
    {
        nowMode = modeCheck();
        time += Time.deltaTime;

        switch (nowMode)
        {
            case Mode.NOTHING: updateNothing(); break;
            case Mode.TOUCH: updateTouch(); break;
            case Mode.SWIPE: updateSwipe(); break;
            case Mode.SEPARATE: updateSeparate(); break;
        }

        if (isTouching())
        {
            foreach (Touch t in Input.touches)
            {	
				touchCheckPlayers (t);
            }
        }

		if (Input.GetMouseButton (0)) {
			Vector2 cursorPos = Input.mousePosition;
			Vector2 worldPos = Camera.main.ScreenToWorldPoint(cursorPos);
			mouseCheckPlayers (worldPos);
		}
    }

	void mouseCheckPlayers(Vector2 mousePos)
	{
		foreach (GameObject player in players) {
			Debug.Log (mousePos);
			//Debug.Log (player.transform.position);
			if (isHitTap (mousePos, player.transform.position, player.GetComponent<Player> ().getRadius())) {
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

    void updateNothing()
    {
        waitFrame++;
    }

    void updateTouch()
    {
         holdFrame++;
        waitFrame = 0;
    }
    void updateSwipe()
    {
        holdFrame++;


    }
    void updateSeparate()
    {
        holdFrame = 0;
    }


    bool isTouching()
    {
        return Input.GetMouseButton(0) || Input.touchCount > 0;
    }


    Mode modeCheck()
    {
        if (isTouching())
        {
            if (holdFrame == 0) return Mode.TOUCH;
            else return Mode.SWIPE;
        }
        else
        {
            if (holdFrame == 0) return Mode.NOTHING;
            else return Mode.SEPARATE;
        }
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
		
		/*if (Mathf.Pow (a.x - b.x, 2) +
		   Mathf.Pow (a.y - b.y, 2)
		   >= Mathf.Pow (2 * radius))
			return true;*/
		return false;
	}
}
