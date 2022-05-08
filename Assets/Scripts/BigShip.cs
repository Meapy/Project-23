using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShip : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Boid>().maxSpeed = 50f;
        gameObject.AddComponent<Constrain>();
        //gameObject.AddComponent<NoiseWander>().axis = NoiseWander.Axis.Horizontal;
        gameObject.AddComponent<NoiseWander>().axis = NoiseWander.Axis.Vertical;

    }

    // Update is called once per frame
    void Update()
    {
        //if health is 0, destroy the game object
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
}
