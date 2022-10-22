using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float tilt;

    public float xMin, xMax, zMin, zMax;

    public GameObject lazerShot;
    public Transform gunPosition;
    public float shotDelay;

    public GameObject smallLazerShot;
    public Transform leftSmallGunPosition;
    public Transform rightSmallGunPosition;
    public float smallShotDelay;
    public GameObject playerExplosion;

    private Rigidbody Ship;
    private GameScript GameScript;

    private float nextShotTime;
    private float nextSmallShotTime;

    // Start is called before the first frame update
    void Start()
    {
        //Ship = GetComponent<Rigidbody>();
        //GameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!GameScript.isStartedAlready())
        //    return;

        //if (Time.time > nextShotTime && Input.GetButton("Fire1"))
        //{
        //    Instantiate(lazerShot, gunPosition.position, Quaternion.identity);
        //    nextShotTime = Time.time + shotDelay;
        //}

        //if (Time.time > nextSmallShotTime && Input.GetButton("Fire2"))
        //{
        //    var leftShot = Instantiate(smallLazerShot, leftSmallGunPosition.position, Quaternion.Euler(0, -45, 0));
        //    leftShot.GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 1) * speed;

        //    var rightShot = Instantiate(smallLazerShot, rightSmallGunPosition.position, Quaternion.Euler(0, 45, 0));
        //    rightShot.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 1) * speed;

        //    nextSmallShotTime = Time.time + smallShotDelay;
        //}

        //var moveHor = Input.GetAxis("Horizontal");
        //var moveVer = Input.GetAxis("Vertical");
        //Ship.velocity = new Vector3(moveHor, 0, moveVer) * speed;

        //Ship.rotation = Quaternion.Euler(tilt * Ship.velocity.z, 0, -tilt * Ship.velocity.x);

        //var posX = Mathf.Clamp(Ship.position.x, xMin, xMax);
        //var posZ = Mathf.Clamp(Ship.position.z, zMin, zMax);
        //Ship.position = new Vector3(posX, Ship.position.y, posZ);

    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Enemy Laser")
        //{
        //    Instantiate(playerExplosion, transform.position, Quaternion.identity);       

        //    Destroy(other.gameObject);
        //    Destroy(gameObject);
        //}
    }
}
