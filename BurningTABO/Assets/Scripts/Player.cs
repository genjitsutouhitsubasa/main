using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    int touchID;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once pessr frame
    void Update () {

    }

    void setTouch(int touchID)
    {
        this.touchID = touchID;
    }

}
