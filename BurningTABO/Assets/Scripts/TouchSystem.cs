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
            Vector2 cursorPos = updateNowPos();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(cursorPos);

            foreach (Touch t in Input.touches)
            {
                Debug.Log(t.phase);
                foreach (GameObject player in players)
                {
                    switch(t.phase)
                    {
                        case TouchPhase.Moved:
                            if (t.fingerId == player.GetComponent<Player>().getTouch())
                            {
                                player.transform.position = worldPos;
                                break;
                            }
                            break;
                        case TouchPhase.Began:
                        case TouchPhase.Stationary:
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            if (isHitTap(worldPos, player.transform.position, 0.3f))
                            {
                                player.transform.position = worldPos;
                                player.GetComponent<Player>().setTouch(t.fingerId);
                            }
                            break;
                    }

                }
            }
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

    Vector3 updateNowPos()
    {
        //UpdatePos (Touch)
        if (Input.touchCount > 0)
            return Input.GetTouch(0).position;

        //UpdatePos (Mouse)
        if (Input.GetMouseButton(0))
            return Input.mousePosition;

        return new Vector3(0, 0, 0);
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

}
