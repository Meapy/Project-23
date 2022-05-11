using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip2 : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    void Awake()
    {
        //attach rigidbody to the spaceship
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;
        //attach the NoiseWander script to the spaceship
        gameObject.AddComponent<NoiseWander>();
        //attach the boid script to the spaceship
        gameObject.AddComponent<Boid>().maxSpeed = 25;
        gameObject.AddComponent<ObstacleAvoidance>();



    }

    // Update is called once per frame
    void Update()
    {

    }
}
