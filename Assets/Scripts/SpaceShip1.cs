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

    }

    // Update is called once per frame
    void Update()
    {
        //if target is null
        if (GetComponent<Pursue>().target == null)
        {
            //set the target to be the closest gameobject with the tag Team2
            GameObject[] Team2 = GameObject.FindGameObjectsWithTag("Team2");
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject Team2Member in Team2)
            {
                Vector3 diff = Team2Member.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = Team2Member;
                    distance = curDistance;
                }
            }
            if (closest != null)
            {
                //set persure target to be closet
                GetComponent<Pursue>().target = closest.GetComponent<Boid>();
            };
        }
        //if health is 0, destroy the game object with the explosion
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity) as GameObject;
            Destroy(explosion, 1f);
        }
        //if distance is less than 1000, spawn bullet
        if (Vector3.Distance(transform.position, GetComponent<Pursue>().target.transform.position) < 1000f)
        {
            if (!spawning)
            {
                spawning = true;
                StartCoroutine(spawnBullet());
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
            yield return new WaitForSeconds(0.5f);
            bullet = Resources.Load("RocketWarhead") as GameObject;
            //add Bullet component to the bullet
            bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            //set size to be 0.2
            bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            bullet.GetComponent<Bullet>().target = closest;
            bullet.GetComponent<Bullet>().speed = 100f;
            //increase the size of the bullet by 2x
            bullet.transform.localScale = new Vector3(2, 2, 2);


        }

    }
}
