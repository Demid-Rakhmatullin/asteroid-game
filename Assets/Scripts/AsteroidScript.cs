using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotationSpeed;
    public float minSpeed, maxSpeed;

    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    private Rigidbody Asteroid;
    private GameScript GameScript;

    // Start is called before the first frame update
    void Start()
    {
        //Asteroid = GetComponent<Rigidbody>();
        //Asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        //Asteroid.velocity = Vector3.back * Random.Range(minSpeed, maxSpeed);

        //GameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "GameBoundary" || other.tag == "Asteroid")
        //{
        //    return;
        //}

        //Instantiate(asteroidExplosion, transform.position, Quaternion.identity);

        //if (other.tag == "Player" || other.tag == "Enemy")
        //{
        //    Instantiate(playerExplosion, other.transform.position, Quaternion.identity);                       
        //}
        //else if (other.tag == "Player Laser")
        //{
        //    GameScript.increaseScore(10);
        //}

        //Destroy(other.gameObject);
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
