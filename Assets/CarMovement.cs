using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        int[] rotations = { 90, -90 };
        transform.rotation = Quaternion.Euler(0, rotations[Random.Range(0, rotations.Length)], 0);

        transform.position += transform.forward * -20;

        speed = Random.Range(0, 120);
        speed = (speed / 100) + 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += transform.forward / 10 * speed;
    }

    private void Update()
    {
        float x = transform.position.x;

        if (x < -20)
        {
            transform.position = new Vector3(20, transform.position.y, transform.position.z);
        }
        else if (x > 20)
        {
            transform.position = new Vector3(-20, transform.position.y, transform.position.z);
        }
    }
}
