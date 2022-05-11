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
            Destroy(this.gameObject);
            //lose hp
            //if target has tag of team1, lose hp
            //if target has tag of team2, lose hp
            if(target.tag == "Team1")
            {
                target.GetComponent<SpaceShip1>().health -= 5;
                print(target.GetComponent<SpaceShip1>().health);
                explosion = Resources.Load("SmallExplosionEffect") as GameObject;
                explosion = GameObject.Instantiate(this.explosion);
                explosion.transform.position = target.transform.position;
                Destroy(explosion, 1f);
            }
            else if(target.tag == "Team2")
            {
                target.GetComponent<SpaceShip2>().health -= 5;
                print(target.GetComponent<SpaceShip2>().health);
                explosion = Resources.Load("SmallExplosionEffect") as GameObject;
                explosion = GameObject.Instantiate(this.explosion);
                explosion.transform.position = target.transform.position;
                Destroy(explosion, 1f);
            }
        }

    }
}
