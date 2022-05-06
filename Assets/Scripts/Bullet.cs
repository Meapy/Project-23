using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 200f;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void KillMe()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        //once it is reached, delete the bullet
        if (transform.position == target.transform.position)
        {

            Destroy(this.gameObject);
            print("Game object destroyed");
            //remove 0.5 tiberium from the base
            target.GetComponent<BigShip>().health -= 1;
        }

        

    }
}
