using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Declared alle variables samen onder Vector 3
    Vector3 up = Vector3.zero,
        currentDirection = Vector3.zero;

    Vector3 nextPos, destination, direction;

    public float speed = 5f;

    private bool canMove = true;

    void Start()
    {
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    void Update()
    {
        Move();

        if (transform.position == destination)
        {
            canMove = true;
        }
    }

    void Move()
    {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                if (canMove == true)
                {
                    nextPos = Vector3.forward;
                    currentDirection = up;
                    destination = transform.position + nextPos;
                    canMove = false;
                }

            }

            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }


}
