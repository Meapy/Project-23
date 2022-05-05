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
        gameObject.AddComponent<NoiseWander>().axis = NoiseWander.Axis.Horizontal;
        gameObject.AddComponent<NoiseWander>().axis = NoiseWander.Axis.Vertical;
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

    }

}
