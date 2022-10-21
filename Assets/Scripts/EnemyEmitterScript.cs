using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitterScript : MonoBehaviour
{
    public float minDelay, maxDelay;
    public GameObject enemy;

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

        if (Time.time > nextSpawn)
        {
            var yPos = transform.position.y;
            var zPos = transform.position.z;
            var xPos = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

            var newPos = new Vector3(xPos, yPos, zPos);

            Instantiate(enemy, newPos, Quaternion.Euler(0, 180, 0));

            nextSpawn = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
