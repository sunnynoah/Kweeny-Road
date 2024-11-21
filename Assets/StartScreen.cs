using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    InputControls controls;
    [SerializeField] PlayerMovement movement;
    [SerializeField] GameObject highScoreDisp;

    [SerializeField] GameObject startUI;
    [SerializeField] GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        controls = new InputControls();
        controls.Enable();

        int score = PlayerPrefs.GetInt("highscore");

        if (score > 0)
        {
            highScoreDisp.GetComponent<TextMeshProUGUI>().text = $"HIGHSCORE\n{score}";
        }
        else
        {
            highScoreDisp.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Player.Any.triggered)
        {
            startUI.SetActive(false);
            UI.SetActive(true);
            movement.canMove = true;
        }
    }
}
