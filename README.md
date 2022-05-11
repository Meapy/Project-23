# Project-23

Name: Daniel Krasovski

Student Number: C18357323

Class Group: DT282

# Description of the project
This project is a space battle simulation. where you can see the space ships fly around and shoot at other 
ships. The aim of this simulation is to destroy all the other ships before they destroy you. multiple behavours were implemented to make this simulation possible. From NoiseWander, to ObstacleAvoidance, persue, seek and others.



# Instructions for use
To view the simulation all you have to do is load the project into unity and run the simulation.




# How it works
## Scene 1
In this scene there are 3 attacking space ships, and two cruising space ships. The cruising space ships are flying around and the attacking space ships are flying towards the cruising space ships and shooting at them. once the distance between the attacking space ships and the cruising space ships is less than 1000 units
**Convoy.cs** 
```C#
    void Awake()
    {
        StartCoroutine(spawnHealth());
        int i = 0;
        //add the Spaceship component to all the child objects
        foreach (Transform child in transform)
        { 
            i++;
            child.gameObject.AddComponent<Spaceship>();
            child.gameObject.AddComponent<ObstacleAvoidance>().weight = 10;
            // in modulo 3 add braking, incident and normal as the force type to obstacle avoidance
            if (i% 3 == 0)
            {
                child.gameObject.GetComponent<ObstacleAvoidance>().forceType = ObstacleAvoidance.ForceType.incident;
                StartCoroutine(ActivatePersue(i,child));
            }
            else if (i % 3 == 1)
            {
                child.gameObject.GetComponent<ObstacleAvoidance>().forceType = ObstacleAvoidance.ForceType.braking;
                StartCoroutine(ActivatePersue(i,child));
            }
            else
            {
                child.gameObject.GetComponent<ObstacleAvoidance>().forceType = ObstacleAvoidance.ForceType.normal;
                StartCoroutine(ActivatePersue(i,child));
            }

        }
    }
```


**Spaceship.cs**
```C#
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
```
**Bigship.cs**
```C#
    void Start()
    {
        gameObject.AddComponent<Boid>().maxSpeed = 36f;
        gameObject.AddComponent<Constrain>().radius = 100f;
        gameObject.AddComponent<NoiseWander>().axis = NoiseWander.Axis.Horizontal;
        gameObject.AddComponent<NoiseWander>().axis = NoiseWander.Axis.Vertical;

    }

    // Update is called once per frame
    void Update()
    {
        //if health is 0, destroy the game object
        if (health <= 0)
        {
            Destroy(this.gameObject);
            explosion = Resources.Load("BigExplosionEffect") as GameObject;
            explosion = GameObject.Instantiate(this.explosion);
            explosion.transform.position = this.transform.position;
            Destroy(explosion, 1f);

        }
        if(health <= 50)
        {
            smoke = Resources.Load("Smoke") as GameObject;
            smoke = GameObject.Instantiate(this.smoke);
            smoke.transform.position = this.transform.position;
            Destroy(smoke, 1f);    
        }  
    }
```
**Bullet.cs**
```C#
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
            target.GetComponent<BigShip>().health -= 5;
            print(target.GetComponent<BigShip>().health);
            explosion = Resources.Load("SmallExplosionEffect") as GameObject;
            explosion = GameObject.Instantiate(this.explosion);
            explosion.transform.position = target.transform.position;
            Destroy(explosion, 1f);
             AudioSource.PlayClipAtPoint(Resources.Load("Explosions/Main") as AudioClip, transform.position);
        }

    }
```
## Scene2
In this scene, there is a battle between 2 different groups of spaceships. They are both flying around and shooting at each other. with one group using pursue and the other using a wander behaviour. SpaceShips are also spawned every 5 seconds at random, and they are randomly assigned to either the group 1 or group 2.
**ConvoySpawner.cs**
```C#
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
```

## Cameras
The cameras are placed beforehand and you can switch between the different cameras by pressing space. 
In Secene 2 there is a camera that rotates around the battle field to show the entire battle field.
```C#
    void Start()
    {
        //get all the objects with the tag camera
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
        print(cameras);
        //set the main camera to the first camera in the array
        cameras[0].GetComponent<Camera>().enabled = true;
        //activate the audio listener
        cameras[0].GetComponent<AudioListener>().enabled = true;
        count = 0;
        //set the rest of the cameras to false
        for (int j = 1; j < cameras.Length; j++)
        {
            cameras[j].GetComponent<Camera>().enabled = false;
            //deactivate the audio listener for the rest of the cameras
            cameras[j].GetComponent<AudioListener>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if space is pressed, switch camera 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
            cameras[count].GetComponent<Camera>().enabled = false;
            cameras[count].GetComponent<AudioListener>().enabled = false;
            count++;
            if (count >= cameras.Length)
            {
                count = 0;
            }
            cameras[count].GetComponent<Camera>().enabled = true;
            cameras[count].GetComponent<AudioListener>().enabled = true;
        }
    }
```

# List of classes/assets in the project and whether made yourself or modified or if its from a source, please give the reference

| Class/asset | Source |
|-----------|-----------|
| BigShip.cs | Self written |
| Convoy.cs | Self written |
| Bullets.cs | Self written |
| CameraControll.cs | Self written |
| ConvoySpawner.cs | Self written |
| SpaceShip.cs | Self written |
| SpaceShip1.cs | Self written |
| SpaceShip2.cs | Self written |
| RotatePlanet.cs | Self written |
All other scripts were taken from https://github.com/skooter500/GE2-2021-2022 and modified to fit the project.
such as: 







# Video:
[![YouTube](https://www.youtube.com/watch?v=RVxdvy0pVrs)](https://www.youtube.com/watch?v=RVxdvy0pVrs)

# Screenshots:
![An image](https://i.gyazo.com/4de85e741e08fb5168ee33a8819c520f.png)

