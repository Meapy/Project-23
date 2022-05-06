using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public int length = 5;
    public Material material;
    private IEnumerator coroutine;

    private GameObject closest;
    public GameObject bullet;

    void Awake()
    {
        gameObject.AddComponent<Boid>();
        gameObject.AddComponent<Seek>();
        gameObject.AddComponent<ObstacleAvoidance>();
        gameObject.AddComponent<Constrain>();
        //set bullet to be the BlueBullet GameObject
        bullet = GameObject.Find("BlueBullet");
    }


    // Start is called before the first frame update
    void Start()
    {
        coroutine = spawnBullet();
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
        Seek seek = this.transform.GetComponent<Seek>();
        seek.targetGameObject = closest;
        if (distance < 600f)
        {
            StartCoroutine(coroutine);
        }
        else
        {
            StopCoroutine(coroutine);
        }

    }
    IEnumerator spawnBullet()
    {
        bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().target = closest;
        //increase the size of the bullet by 2x
        bullet.transform.localScale = new Vector3(2, 2, 2);
        yield return new WaitForSeconds(1);
    }

}
