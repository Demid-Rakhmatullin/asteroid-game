using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerScript : MonoBehaviour
{
    public float speed;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var shift = Mathf.Repeat(Time.time * speed, transform.localScale.y);
        transform.position = startPos + Vector3.back * shift;
    }
}
