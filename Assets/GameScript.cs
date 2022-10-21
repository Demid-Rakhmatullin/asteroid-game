using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject scoreLabel;
    public GameObject startButton;
    public GameObject menu;

    private bool isStarted = false;
    private int score = 0;

    public bool isStartedAlready()
    {
        return isStarted;
    }

    public void increaseScore(int increment)
    {
        score += increment;
        scoreLabel.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            menu.SetActive(false);
            isStarted = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
