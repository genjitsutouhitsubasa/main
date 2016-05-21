using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    int touchID;

    // Use this for initialization
    void Start () {
        touchID = -1;
    }
    
    // Update is called once pessr frame
    void Update () {

    }

    public void setTouch(int touchID)
    {
        this.touchID = touchID;
    }

    public int getTouch()
    {
        return this.touchID;
    }
}
