using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfo : MonoBehaviour
{
    public GameObject MainParent;
    public GameObject Right;
    public GameObject Left;
    public GameObject RightPlane;
    public GameObject LeftPlane;

    private void Start() {
        MainParent = transform.parent.gameObject;
        Vector3 size = transform.localScale;

        Debug.Log(size);

        Right.transform.localPosition = new Vector3(size.x / 2 , 0 , 0);
        RightPlane.transform.localPosition = new Vector3(size.x / 2 , size.y / 2 , 0);

        Left.transform.localPosition = new Vector3(-size.x / 2 , 0 , 0);
        LeftPlane.transform.localPosition = new Vector3(-size.x / 2 , size.y / 2 , 0);

        transform.position = new Vector3(transform.position.x , size.y / 2 , transform.position.z);
    }
}
