using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfo : MonoBehaviour
{
    public GameObject MainParent;
    public GameObject Right;
    public GameObject Left;

    private void Start() {
        MainParent = transform.parent.gameObject;
    }
}
