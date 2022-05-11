using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    private IEnumerator coroutine;
    private GameObject closest;
    public GameObject bullet;
    private bool spawning = false;
    public int health = 100;

    void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        coroutine = spawnBullet();
        this.transform.GetComponent<Pursue>().enabled = false;
        this.transform.GetComponent<Boid>().maxForce = 120;
        this.transform.GetComponent<Boid>().maxSpeed = 35;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] BigShips = GameObject.FindGameObjectsWithTag("BigShip");
        closest = null;
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
        if (closest != null)
        {
            //set persure target to be closet
            this.transform.GetComponent<Pursue>().target = closest.GetComponent<Boid>();
        }

        //spawn bullet once distance is less than 1000
        if (distance < 1500f)
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
        //if BigShips 
        if (BigShips.Length == 0)
        {
            //load next scene
            SceneManager.LoadScene("Scene2");

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
            AudioSource.PlayClipAtPoint(Resources.Load("Explosions/ShootSound") as AudioClip, transform.position);
        }

    }

}
