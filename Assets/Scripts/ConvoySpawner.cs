using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public int team = 1;
    public int radius = 100;
    void Start()
    {
        // spawn space ship objects at the start of the game as a child of the convoy object
        for (int j = 0; j < 5; j++)
        {
            for (int i = 1; i <= 6; i++)
            {
                //if team is 1, spawn the ships that are in the team1 folder in resources
                if (team == 1)
                {
                    GameObject ship = Resources.Load("TeamOne/Ship" + i) as GameObject;
                    ship = GameObject.Instantiate(ship);
                    //set the position of the ship to a random point within the radius
                    ship.transform.position = new Vector3(Random.Range(-radius, radius) - 400, 0, Random.Range(-radius, radius));
                    //set the parent of the ship to the convoy object
                    ship.transform.parent = this.transform;
                    //add the tag Team1 to the ship
                    ship.tag = "Team1";
                    //add the Spaceship component to the ship
                    ship.AddComponent<SpaceShip1>();
                    ship.GetComponent<SpaceShip1>().enabled = true;
                }
                //if team is 2, spawn the ships that are in the team2 folder in resources
                else if (team == 2)
                {
                    GameObject ship = Resources.Load("TeamTwo/Prefab" + i) as GameObject;
                    ship = GameObject.Instantiate(ship);
                    //set the position of the ship to a random point within the radius
                    ship.transform.position = new Vector3(Random.Range(-radius, radius) + 400, 0, Random.Range(-radius, radius));
                    //set the parent of the ship to the convoy object
                    ship.transform.parent = transform;
                    //set size to be 0.1
                    ship.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    //add the tag Team2 to the ship
                    ship.tag = "Team2";
                    //add the Spaceship component to the ship
                    ship.AddComponent<SpaceShip2>();
                }
            }
        }
        //start the Enumerator SpawnShips
        StartCoroutine(SpawnShips());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnShips()
    {
        //spawn more ships at random every 10 seconds
        while (true)
        {
            yield return new WaitForSeconds(5f);
            //random between 1 and 2
            int random = Random.Range(1, 3);
            print(random);
            //if team is 1, spawn the ships that are in the team1 folder in resources
            if (team == 1 && random == 1)
            {
                GameObject ship = Resources.Load("TeamOne/Ship" + Random.Range(1, 6)) as GameObject;
                ship = GameObject.Instantiate(ship);
                //set the position of the ship to a random point within the radius
                ship.transform.position = new Vector3(Random.Range(-radius, radius) - 400, 0, Random.Range(-radius, radius));
                //set the parent of the ship to the convoy object
                ship.transform.parent = this.transform;
                //add the tag Team1 to the ship
                ship.tag = "Team1";
                //add the Spaceship component to the ship
                ship.AddComponent<SpaceShip1>();
            }
            //if team is 2, spawn the ships that are in the team2 folder in resources
            else if (team == 2 && random == 2)
            {
                GameObject ship = Resources.Load("TeamTwo/Prefab" + Random.Range(1, 6)) as GameObject;
                ship = GameObject.Instantiate(ship);
                //set the position of the ship to a random point within the radius
                ship.transform.position = new Vector3(Random.Range(-radius, radius) + 400, 0, Random.Range(-radius, radius));
                //set the parent of the ship to the convoy object
                ship.transform.parent = transform;
                //set size to be 0.1
                ship.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                //add the tag Team2 to the ship
                ship.tag = "Team2";
                //add the Spaceship component to the ship
                ship.AddComponent<SpaceShip2>();
            }
        }
    }
}
