using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfo : MonoBehaviour
{
    public GameObject MainParent;
    public GameObject RightPivot;
    public GameObject LeftPivot;
    public GameObject RightPlane;
    public GameObject LeftPlane;

    private void Start() {
        MainParent = transform.parent.gameObject;
        Vector3 size = transform.localScale;

        RightPivot.transform.localPosition = new Vector3(size.x / 2 , 0 , 0);
        RightPlane.transform.localPosition = new Vector3(size.x / 2 , size.y / 2 , 0);

        LeftPivot.transform.localPosition = new Vector3(-size.x / 2 , 0 , 0);
        LeftPlane.transform.localPosition = new Vector3(-size.x / 2 , size.y / 2 , 0);

        transform.position = new Vector3(transform.position.x , size.y / 2 , transform.position.z);

        ScaleControl rightScale = RightPlane.GetComponent<ScaleControl>();
        ScaleControl leftScale = LeftPlane.GetComponent<ScaleControl>();
        
        rightScale.pivot = RightPivot.transform;
        rightScale.secondPivot = LeftPivot.transform;
        rightScale.wall = this.gameObject.transform;

        
        leftScale.pivot = LeftPivot.transform;
        leftScale.secondPivot = RightPivot.transform;
        leftScale.wall = this.gameObject.transform;

        rightScale.enabled = true;
        leftScale.enabled = true;
    }
}
