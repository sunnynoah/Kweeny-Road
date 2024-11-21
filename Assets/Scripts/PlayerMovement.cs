using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int score;
    [SerializeField] TextMeshProUGUI scoreDisp;

    InputControls controls;


    Vector3 up = Vector3.zero,
    currentDirection = Vector3.zero;

    Vector3 nextPos, destination, direction;

    public float speed = 5f;

    private bool moving = false;
    public bool canMove = true;

    [SerializeField] private LaneSpawner spawner;

    void Start()
    {
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
        controls = new InputControls();
        controls.Enable();
    }

    void Update()
    {
        Move();

        if (transform.position == destination)
        {
            moving = false;
        }
    }

    void Move()
    {
            if (controls.Player.Move.triggered && canMove)
            {
                if (!moving)
                {
                    score++;
                    scoreDisp.text = score.ToString();
                    spawner.GenerateLane();
                    nextPos = Vector3.forward;
                    currentDirection = up;
                    destination = transform.position + nextPos;
                    moving = true;
                }

            }

            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }


}
