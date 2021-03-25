using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir{
    Top,
    Bottom,
    Right,
    Left,
    Front,
    Back
}
public class Snapping : MonoBehaviour
{
    public Dir direction;
    public MoveableInfo moveableInfo;
    public GameObject snappObject;
    void Start()
    {
        moveableInfo = GetComponentInParent<MoveableInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Wall")
            moveableInfo.relativeWall = other.gameObject;

    }
}
