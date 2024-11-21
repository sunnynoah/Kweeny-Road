using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerDeath : MonoBehaviour
{
    float nextCooldown;
    InputControls controls;
    private bool alive = true;
    [SerializeField] PlayerMovement movement;

    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject regularUI;
    [SerializeField] TextMeshProUGUI finalScore;

    [SerializeField] GameObject deathEffect;

    private void Start()
    {
        controls = new InputControls();
        controls.Enable();
    }
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
            nextCooldown = 0.5f;
            Instantiate(deathEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            int score = movement.score;
            if (score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            alive = false;
            transform.localScale = new Vector3(1.2f, 0.1f, 1.2f);
            movement.canMove = false;

            regularUI.SetActive(false);
            deathUI.SetActive(true);
            finalScore.text = score.ToString(); 
        }
    }

    private void Update()
    {
        if (nextCooldown <= 0)
        {
            if (!alive && controls.Player.Any.triggered)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            nextCooldown -= Time.deltaTime;
        }
    }
}
