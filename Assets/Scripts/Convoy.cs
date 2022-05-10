using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convoy : MonoBehaviour
{
    public GameObject health;
    private int cameraCount;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(spawnHealth());
        int i = 0;
        //add the Spaceship component to all the child objects
        foreach (Transform child in transform)
        { 
            i++;
            child.gameObject.AddComponent<Spaceship>();
            child.gameObject.AddComponent<ObstacleAvoidance>().weight = 10;
            //child.gameObject.AddComponent<Constrain>().radius = 200f;
            // in modulo 3 add braking, incident and normal as the force type to obstacle avoidance
            if (i% 3 == 0)
            {
                child.gameObject.GetComponent<ObstacleAvoidance>().forceType = ObstacleAvoidance.ForceType.incident;
                StartCoroutine(ActivatePersue(i,child));
            }
            else if (i % 3 == 1)
            {
                child.gameObject.GetComponent<ObstacleAvoidance>().forceType = ObstacleAvoidance.ForceType.braking;
                StartCoroutine(ActivatePersue(i,child));
            }
            else
            {
                child.gameObject.GetComponent<ObstacleAvoidance>().forceType = ObstacleAvoidance.ForceType.normal;
                StartCoroutine(ActivatePersue(i,child));
            }

        }
        //get all the objects with the tag camera
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
        print(cameras);
        //set the main camera to the first camera in the array
        cameras[0].GetComponent<Camera>().enabled = true;
        cameraCount = 0;
        //set the rest of the cameras to false
        for (int j = 1; j < cameras.Length; j++)
        {
            cameras[j].GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if space is pressed, switch camera 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
            cameras[cameraCount].GetComponent<Camera>().enabled = false;
            cameraCount++;
            if (cameraCount >= cameras.Length)
            {
                cameraCount = 0;
            }
            cameras[cameraCount].GetComponent<Camera>().enabled = true;
        }

    }
    IEnumerator spawnHealth()
    {
        yield return new WaitForSeconds(5f);
        //instantiate health
        GameObject health = GameObject.Instantiate(this.health);
        //set the position to spawn at random within the spawn radius
        Vector2 position = Random.insideUnitCircle * 20;
        //set the hight to be random 
        float height = Random.Range(0, 10);
        //set the position of the health
        health.transform.position = new Vector3(position.x, height, position.y);
        StartCoroutine(spawnHealth());
    }
    IEnumerator ActivatePersue(int i,Transform child)
    {
        yield return new WaitForSeconds(i);
        child.gameObject.GetComponent<Pursue>().enabled = true;
    }
}
