using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithDrag : MonoBehaviour
{
    public Transform worldAnchor;
    public Vector3 initialScale;
    public float CameraZDistance;
    public Camera mainCamera;
    void Start()
    {
        initialScale = transform.localScale;
        mainCamera = Camera.main;
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = CameraZDistance;

            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

            float distance = Vector3.Distance(worldAnchor.position , mouseWorldPos);
            transform.localScale = new Vector3(initialScale.x , distance / 2 , initialScale.z);

            Vector3 middlePoint = (worldAnchor.position + mouseWorldPos) / 2;
            transform.position = middlePoint;

            Vector3 rotation = (mouseWorldPos - worldAnchor.position);
            transform.up = rotation;
        }
    }

    private void OnMouseDrag() {
        
    }
}
