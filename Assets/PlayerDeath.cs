using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerDeath : MonoBehaviour
{
    InputControls controls;
    private bool alive = true;
    [SerializeField] PlayerMovement movement;

    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject regularUI;
    [SerializeField] TextMeshProUGUI finalScore;

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
            int score = movement.score;
            if (score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            alive = false;
            transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.y);
            movement.canMove = false;

            regularUI.SetActive(false);
            deathUI.SetActive(true);
            finalScore.text = score.ToString(); 
        }
    }

    private void Update()
    {
        if (!alive && controls.Player.Any.triggered)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
