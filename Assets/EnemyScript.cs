using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    //public float tilt;

    public GameObject lazerShot;
    public Transform gunPosition;
    public float shotDelay;
    public GameObject playerExplosion;

    private Rigidbody Enemy;
    private GameScript GameScript;

    private float nextShotTime;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<Rigidbody>();
        Enemy.velocity = Vector3.back * speed;

        GameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(lazerShot, gunPosition.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Player Laser" || other.tag == "Enemy")
        {
            Instantiate(playerExplosion, transform.position, Quaternion.identity);

            if (other.tag == "Player Laser")
                GameScript.increaseScore(50);
            if (other.tag == "Player")
                Instantiate(playerExplosion, other.transform.position, Quaternion.identity);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
