using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public int length = 5;
    public Material material;

    void Awake()
    {        
        gameObject.AddComponent<Boid>();
        gameObject.AddComponent<Seek>();
        gameObject.AddComponent<ObstacleAvoidance>();
        gameObject.AddComponent<Constrain>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] BigShips = GameObject.FindGameObjectsWithTag("BigShip");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject BigShip in BigShips)
        {
            Vector3 diff = BigShip.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = BigShip;
                distance = curDistance;
            }
        }
        Seek seek = this.transform.GetComponent<Seek>();
        seek.targetGameObject = closest;

    }

}
