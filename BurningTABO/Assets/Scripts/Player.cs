using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    int touchID;
	[SerializeField]float radius;
	[SerializeField]int group;

    // Use this for initialization
    void Start () {
        touchID = -1;
    }
    
    // Update is called once pessr frame
    void Update () {

    }

	public void Move(Vector2 point) {
		this.transform.position = point;
	}

    public void setTouch(int touchID)
    {
        this.touchID = touchID;
    }

    public int getTouch()
    {
        return this.touchID;
    }

	public float getRadius()
	{
		return radius;
	}
	public int getGroup()
	{
		return group;
	}
}
