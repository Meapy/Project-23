using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private IEnumerator coroutine;
    private GameObject closest;
    public GameObject bullet;
    private bool spawning = false;

    void Awake()
    {
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
        //two seeks, needs to be fixed, probably not tho
        Seek seek = this.transform.GetComponent<Seek>();
        seek.targetGameObject = closest;
        //spawn bullet once distance is less than 1000
        if (distance < 1000)
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
            yield return new WaitForSeconds(1f);
            bullet = Resources.Load("BlueBullet") as GameObject;
            bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Bullet>().target = closest;
            bullet.GetComponent<Bullet>().speed = 100f;
            //increase the size of the bullet by 2x
            bullet.transform.localScale = new Vector3(2, 2, 2);
            

        }

    }

}
