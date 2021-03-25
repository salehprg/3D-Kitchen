using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableInfo : MonoBehaviour
{
    public bool Wall;
    public bool Floor;
    public bool Roof;
    public GameObject Pivot;
    public GameObject snappObj;
    public GameObject relativeWall;
    public float rotateOffset = 180f;

    Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        float minDist = int.MaxValue;

        foreach (var wall in walls)
        {
            float distance = Vector3.Distance(transform.position , wall.transform.position);

            if(distance < minDist)
            {
                minDist = distance;
                relativeWall = wall;
            }
        }
        
        Snapping[] snapps = GetComponentsInChildren<Snapping>();

        if(Roof)
        {
            foreach (var comp in snapps)
            {
                if(comp.direction == Dir.Top)
                    Pivot = comp.gameObject;
            }
            GameObject RoofGO = GameObject.FindGameObjectWithTag("Roof");
            transform.position = new Vector3(transform.position.x , RoofGO.transform.position.y , transform.position.z);
        }
        if(Wall)
        {
            transform.position = new Vector3(relativeWall.transform.position.x , transform.position.y , relativeWall.transform.position.z);
        }
        if(Floor)
        {
            transform.position = new Vector3(transform.position.x , collider.bounds.size.y / 2 , transform.position.z);
        }

        if(Pivot == null)
        {
            foreach (var comp in snapps)
            {
                if(comp.direction == Dir.Back)
                    Pivot = comp.gameObject;
            }
        }
        
        MoveObjectToTarget(transform.position , new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveObjectToTarget(Vector3 destination , Vector3 normalSurface)
    {
        if(relativeWall != null)
        {
            transform.transform.eulerAngles = new Vector3(relativeWall.transform.eulerAngles.x
                                                            ,relativeWall.transform.eulerAngles.y - rotateOffset
                                                            ,relativeWall.transform.eulerAngles.z);
        }

        Vector3 newPos = Vector3.Slerp(Pivot.transform.position , destination , 100 * Time.deltaTime); 

        transform.position = newPos;
        transform.position -= Pivot.transform.position - transform.position;

        float angle = Vector3.SignedAngle(transform.forward , normalSurface , Vector3.up);
        Debug.Log(angle);

        if(angle == 180)
            rotateOffset = 180;

        if(angle == -180)
            rotateOffset = 0;
    }
}
