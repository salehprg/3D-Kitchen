using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableInfo : MonoBehaviour
{
    public bool Wall;
    public bool Floor;
    public bool Roof;

    public GameObject snappObj;
    public GameObject relativeWall;
    void Start()
    {
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
        
        if(Roof)
        {
            GameObject RoofGO = GameObject.FindGameObjectWithTag("Roof");
            transform.position = new Vector3(transform.position.x , RoofGO.transform.position.y , transform.position.z);
        }
        if(Wall)
        {
            transform.position = new Vector3(relativeWall.transform.position.x , transform.position.y , relativeWall.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveObjectToTarget(GameObject target , Vector3 destination)
    {
        Collider collider = GetComponent<Collider>();
        Debug.Log(collider.bounds.size.y);
        

        float angle = Vector3.Angle(relativeWall.transform.forward , Vector3.forward);
        float rX = Mathf.Sin(angle);
        float rZ = Mathf.Cos(angle);

        destination.x = destination.x - (rX * collider.bounds.size.x / 2);
        destination.y = collider.bounds.size.y / 2;
        destination.z = destination.z - (rZ * collider.bounds.size.z / 2);

        Debug.Log("X : " + rX + " Z : " + rZ);
        
        if(relativeWall != null)
        {
            transform.transform.eulerAngles = new Vector3(relativeWall.transform.eulerAngles.x
                                                            ,relativeWall.transform.eulerAngles.y - 180
                                                            ,relativeWall.transform.eulerAngles.z);
        }

        transform.position = Vector3.Lerp(transform.position , destination , 100 * Time.deltaTime); 
    }
}
