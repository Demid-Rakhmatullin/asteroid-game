using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEmitterScript : MonoBehaviour
{
    public float minDelay, maxDelay;
    public GameObject asteroid1, asteroid2, asteroid3;

    private GameScript GameScript;

    private float nextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        GameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameScript.isStartedAlready())
            return;

        //if (!GameStaticModel.GameStarted.Value)
        //    return;

        if (Time.time > nextSpawn)
        {
            var yPos = transform.position.y;
            var zPos = transform.position.z;
            var xPos = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

            var newPos = new Vector3(xPos, yPos, zPos);

            var asteroidType = Random.Range(1, 4);
            GameObject asteroid;
            switch(asteroidType)
            {
                case 1:
                    asteroid = asteroid1;
                    break;
                case 2:
                    asteroid = asteroid2;
                    break;
                default:
                    asteroid = asteroid3;
                    break;
            }

            Instantiate(asteroid, newPos, Quaternion.identity);

            nextSpawn = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
