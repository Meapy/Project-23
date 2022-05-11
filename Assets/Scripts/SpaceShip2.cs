using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip2 : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    private GameObject explosion;
    private bool spawning = false;
    private GameObject smoke;
    private GameObject bullet;
    private GameObject target = null;
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
        //add the Constraint script to the spaceship
        gameObject.AddComponent<Constrain>().radius = 500f;

    }

    // Update is called once per frame
    void Update()
    {
        //if health is 0, destroy the game object with an explosion
        //set the target to be a random gameobject with the tag Team2 if it doesn not have a target
        if (target == null)
        {
            GameObject[] Team1 = GameObject.FindGameObjectsWithTag("Team1");
            int random = Random.Range(0, Team1.Length);
            target = Team1[random];
        }
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
        //get distance between the spaceship and the target
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        //if distance is less than 1000, spawn bullet
        if (distance < 1000f)
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

    }
    IEnumerator spawnBullet()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(2f);
            bullet = Resources.Load("RocketWarheadRed") as GameObject;
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
