using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool alive = true;
    [SerializeField] PlayerMovement movement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            print("The car violently entered your body. You died!");
            Death("Car");
        }
    }

    public void Death(string cod)
    {
        if (alive)
        {
            alive = false;
            transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.y);
            movement.canMove = false;
        }
    }
}
