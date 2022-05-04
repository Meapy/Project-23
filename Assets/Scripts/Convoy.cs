using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convoy : MonoBehaviour
{
    public GameObject prefab;
    public GameObject health;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(spawnHealth());
        //add the Spaceship component to all the child objects
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<Spaceship>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator spawnHealth()
    {
        yield return new WaitForSeconds(5f);
        //instantiate health
        GameObject health = GameObject.Instantiate(this.health);
        //set the position to spawn at random within the spawn radius
        Vector2 position = Random.insideUnitCircle * 20;
        //set the hight to be random 
        float height = Random.Range(0, 10);
        //set the position of the health
        health.transform.position = new Vector3(position.x, height, position.y);
        
        StartCoroutine(spawnHealth());
    }
}
