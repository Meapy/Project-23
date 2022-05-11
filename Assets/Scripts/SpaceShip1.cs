using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip1 : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    private bool spawning = false;
    public GameObject bullet;
    private GameObject closest;
    private GameObject target;
    private GameObject explosion;
    private GameObject smoke;
    void Awake()
    {
        //attach rigidbody to the spaceship
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;
        //attach the persue script to the spaceship
        gameObject.AddComponent<Pursue>();
        //attach the boid script to the spaceship
        gameObject.AddComponent<Boid>().maxSpeed = 25;
        gameObject.AddComponent<ObstacleAvoidance>();
        //set target to be a random gameobject with the tag Team2
        
        GameObject[] Team2 = GameObject.FindGameObjectsWithTag("Team2");
        int random = Random.Range(0, Team2.Length);
        GetComponent<Pursue>().target = Team2[random].GetComponent<Boid>();
        //set target to be that gameobject
        target = Team2[random];



    }

    // Update is called once per frame
    void Update()
    {
        //set the target to be a random gameobject with the tag Team2 if it doesn not have a target
        if (GetComponent<Pursue>().target == null)
        {
            GameObject[] Team2 = GameObject.FindGameObjectsWithTag("Team2");
            int random = Random.Range(0, Team2.Length);
            GetComponent<Pursue>().target = Team2[random].GetComponent<Boid>();
            target = Team2[random];
        }
        //get distance between the spaceship and the target
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        //spawn bullet once distance is less than 1000
        if (distance < 1500f)
        {
            if (!spawning)
            {
                spawning = true;
                StartCoroutine(spawnBullet());
                print("swapnings");
            }
        }
        else
        {
            spawning = false;

        }
        //if health is 0, destroy the game object with an explosion
        if (health <= 0)
        {
            Destroy(this.gameObject);
            explosion = Resources.Load("ExplosionEffect") as GameObject;
            explosion = GameObject.Instantiate(this.explosion);
            explosion.transform.position = this.transform.position;
            Destroy(explosion, 1f);

        }
        //if health is less than 50, spawn smoke
        if (health <= 50)
        {
            smoke = Resources.Load("Smoke") as GameObject;
            smoke = GameObject.Instantiate(this.smoke);
            smoke.transform.position = this.transform.position;
            Destroy(smoke, 1f);
        }
    }
    IEnumerator spawnBullet()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(1f);
            bullet = Resources.Load("RocketWarheadGreen") as GameObject;
            //add Bullet component to the bullet
            bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            //set size to be 0.2
            bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            bullet.GetComponent<Bullets>().target = target;
            bullet.GetComponent<Bullets>().speed = 100f;
            //increase the size of the bullet by 2x
            bullet.transform.localScale = new Vector3(2, 2, 2);

        }

    }
}
