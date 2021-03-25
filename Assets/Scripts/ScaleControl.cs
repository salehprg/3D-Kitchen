using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleControl : MonoBehaviour
{
    
    [HideInInspector]
    public Transform pivot;

    [HideInInspector]
    public Transform secondPivot;

    [HideInInspector]
    public Transform wall;

    Vector3 initialScale;
    Camera mainCamera;
    Vector3 mouseWorldPos;

    void Start()
    {
        initialScale = wall.localScale;
        mainCamera = GameObject.FindGameObjectWithTag("2D Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(secondPivot != null)
        {
            transform.position = new Vector3(secondPivot.position.x , transform.position.y , secondPivot.position.z);
        }
    }

    Vector3 refrenceVector;
    Vector3 startPos;
    WallInfo wallInfo;
    float dist;
    private void OnMouseDown() {
        startPos = GetMouseInWorld();
        wallInfo = wall.GetComponent<WallInfo>();

        wall.SetParent(pivot.transform);
        secondPivot.SetParent(wall);

        initialScale = pivot.localScale;

        dist = Vector3.Distance(secondPivot.position , pivot.position);
    }

    private void OnMouseUp() {
        wall.SetParent(wallInfo.MainParent.transform);
        secondPivot.SetParent(wallInfo.MainParent.transform);
        
        initialScale = wall.localScale;
    }

    private void OnMouseDrag() 
    {
        Vector3 _secondPivot = secondPivot.position;
        _secondPivot.y = 0;

        Vector3 _pivot = pivot.position;
        _pivot.y = 0;

        refrenceVector = _secondPivot - _pivot;
        
        mouseWorldPos = GetMouseInWorld();


        float angleRot = Vector3.SignedAngle(refrenceVector , mouseWorldPos - _pivot , wall.up);
        pivot.localEulerAngles += new Vector3(0 , angleRot , 0);

        float dist2 = Vector3.Distance(_pivot , mouseWorldPos);
        //Debug.Log(dist2 / dist);

        pivot.localScale = new Vector3(initialScale.x * dist2 / dist , initialScale.y , initialScale.z);
        
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
