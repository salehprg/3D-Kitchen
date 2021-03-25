using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject Roof;
    public GameObject touched;
    public MoveableInfo moveable;
    public Collider coll;
    public Vector3 lastPos;
    Vector3 targetPos = new Vector3();
    public LayerMask IgnoreRaycast;
    public LayerMask WallRayLayer;
    public LayerMask RoofRayLayer;
    public LayerMask FloorRayLayer;
    public int FloorLayerIndex;
    public int WallLayerIndex;

    void Start()
    {
        Roof = GameObject.FindGameObjectWithTag("Roof");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit[] hits = Physics.RaycastAll(ray.origin , ray.direction );
            if(hits.Length > 0)
            {
                foreach(var hittedObject in hits)
                {
                    moveable = hittedObject.transform.gameObject.GetComponent<MoveableInfo>();

                    if(moveable != null && touched == null)
                    {
                        touched = hittedObject.transform.gameObject;
                        break;
                    }
                }
            }
        }
        if(Input.GetMouseButton(0) && touched != null)
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit = new RaycastHit();
            bool wall = false , floor = false , roof = false;

            if(moveable.Wall)
                wall = true;
            if(moveable.Floor)
                floor = true;
            if(moveable.Roof)
                roof = true;
            
            if(wall)
            {
                if(Physics.Raycast(ray.origin , ray.direction , out hit , float.MaxValue , WallRayLayer))
                {
                    targetPos = new Vector3(hit.point.x , hit.point.y , hit.point.z);
                    
                    if(hit.transform.gameObject.layer == FloorLayerIndex)
                        targetPos = new Vector3(targetPos.x , touched.transform.position.y , hit.point.z);
                }
            }
            if(floor)
            {
                if(Physics.Raycast(ray.origin , ray.direction , out hit , float.MaxValue ,  FloorRayLayer))
                {
                    targetPos = new Vector3(hit.point.x , touched.transform.position.y , hit.point.z);

                    if(hit.transform.gameObject.layer == WallLayerIndex)
                        targetPos = new Vector3(hit.point.x , targetPos.y , hit.point.z);
                }
            }
            if(roof)
            {
                if(Physics.Raycast(ray.origin , ray.direction , out hit , float.MaxValue , RoofRayLayer))
                {
                    targetPos = new Vector3(hit.point.x , hit.point.y , hit.point.z);
                }
            }

            if(hit.transform != null && hit.transform.gameObject.tag == "Wall")
                moveable.relativeWall = hit.transform.gameObject;

            moveable.MoveObjectToTarget(targetPos , hit.normal);
        }
        if(Input.GetMouseButtonUp(0))
        {
            touched = null;
            moveable = null;
        }
    }

    
    
}
