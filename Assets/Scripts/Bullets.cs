using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed = 200f;
    public GameObject target = null;
    // Start is called before the first frame update
    public GameObject explosion;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move towards the target
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //once it is reached, delete the bullet
        if (transform.position == target.transform.position)
        {
            
            if(target.tag == "Team1")
            {
                target.GetComponent<SpaceShip1>().health -= 2;
                print(target.GetComponent<SpaceShip1>().health);
                explosion = Resources.Load("SmallExplosionEffect") as GameObject;
                explosion = GameObject.Instantiate(this.explosion);
                explosion.transform.position = target.transform.position;
                Destroy(explosion, 1f);
            }
            else if(target.tag == "Team2")
            {
                target.GetComponent<SpaceShip2>().health -= 2;
                print(target.GetComponent<SpaceShip2>().health);
                explosion = Resources.Load("SmallExplosionEffect") as GameObject;
                explosion = GameObject.Instantiate(this.explosion);
                explosion.transform.position = target.transform.position;
                Destroy(explosion, 1f);
            }
            Destroy(this.gameObject);
        }

    }
}
