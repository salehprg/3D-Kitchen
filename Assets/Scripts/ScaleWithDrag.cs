using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithDrag : MonoBehaviour
{
    public Transform wall;
    public Vector3 initialScale;
    public Vector3 initialPos;
    public Camera mainCamera;
    public Vector3 mouseWorldPos;

    void Start()
    {
        initialScale = wall.localScale;
        initialPos = wall.position;
        mainCamera = GameObject.FindGameObjectWithTag("2D Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(right != null)
        {
            transform.position = new Vector3(right.position.x , transform.position.y , right.position.z);
        }
    }

    public Vector3 refrenceVector;
    Transform right;
    Transform left;
    Vector3 startPos;
    WallInfo wallInfo;
    float dist;
    private void OnMouseDown() {
        startPos = GetMouseInWorld();
        wallInfo = wall.GetComponent<WallInfo>();
        right = wallInfo.Right.transform;
        left = wallInfo.Left.transform;

        wall.SetParent(wallInfo.Left.transform);
        right.SetParent(wall);

        dist = Vector3.Distance(right.position , left.position);
    }

    private void OnMouseUp() {
        //wall.SetParent(wallInfo.MainParent.transform);
    }

    private void OnMouseDrag() 
    {
        Vector3 _right = right.position;
        _right.y = 0;

        Vector3 _left = left.position;
        _left.y = 0;

        refrenceVector = _right - _left;
        
        mouseWorldPos = GetMouseInWorld();


        float angleRot = Vector3.SignedAngle(refrenceVector , mouseWorldPos - _left , wall.up);
        left.localEulerAngles += new Vector3(0 , angleRot , 0);

        float dist2 = Vector3.Distance(_left , mouseWorldPos);
        Debug.Log(dist2 / dist);

        left.localScale = new Vector3(initialScale.x * dist2 / dist , initialScale.y , initialScale.z);
        
    }

    Vector3 GetMouseInWorld()
    {
        Vector3 result = new Vector3();

        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray.origin , ray.direction , out hit))
        {
            result = new Vector3(hit.point.x , 0 , hit.point.z);
        }

        return result;
    }
}
