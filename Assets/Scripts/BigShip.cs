using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShip : MonoBehaviour
{
    public int health = 100;
    public GameObject smoke;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Boid>().maxSpeed = 36f;
        gameObject.AddComponent<Constrain>().radius = 100f;
        gameObject.AddComponent<NoiseWander>().axis = NoiseWander.Axis.Horizontal;
        gameObject.AddComponent<NoiseWander>().axis = NoiseWander.Axis.Vertical;

    }

    // Update is called once per frame
    void Update()
    {
        //if health is 0, destroy the game object
        if (health <= 0)
        {
            Destroy(this.gameObject);
            explosion = Resources.Load("BigExplosionEffect") as GameObject;
            explosion = GameObject.Instantiate(this.explosion);
            explosion.transform.position = this.transform.position;
            Destroy(explosion, 1f);

        }
        if(health <= 50)
        {
            smoke = Resources.Load("Smoke") as GameObject;
            smoke = GameObject.Instantiate(this.smoke);
            smoke.transform.position = this.transform.position;
            Destroy(smoke, 1f);    
        }
        //if game object with tag health exists, go towards the the gameobject

        //on collision with game object, increase hp by 2 and delete the health game object
        
    }
}
