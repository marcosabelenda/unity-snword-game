using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewGame : MonoBehaviour
{
    public static float MAX_TIME = 4f;

    public bool isActive = false;
    public bool isRestart = true;
    public float time = 0f;
    public bool firstActive = true;
    public KeyCode actionKey;
    public GameObject restartGameUI;
    public GameObject nextGameUI;


    // Start is called before the first frame update
    void Start()
    {
        restartGameUI.SetActive(false);
        nextGameUI.SetActive(false);
        resetValues();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && time < MAX_TIME)
        {
            time += Time.deltaTime;
        } else if(isActive && time > MAX_TIME)
        {
            if (firstActive && isRestart)
            {
                restartGameUI.SetActive(true);
                firstActive = false;
            }
            else if (firstActive && !isRestart)
            {
                nextGameUI.SetActive(true);
                firstActive = false;
            }

            if (Input.GetKeyDown(actionKey) && isRestart)
            {
                SceneManager.LoadScene("Game");
            } else if (Input.GetKeyDown(actionKey) && !isRestart)
            {
                if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.EASY)
                {
                    GameConfiguration.gameConfiguration.mediumGameConfiguration();
                    SceneManager.LoadScene("Tower");
                }
                else if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.MEDIUM)
                {
                    GameConfiguration.gameConfiguration.hardGameConfiguration();
                    SceneManager.LoadScene("Tower");
                }
                else if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.HARD)
                {
                    SceneManager.LoadScene("Win");
                }
            }
        }
    }

    private void resetValues()
    {
        isActive = false;
        isRestart = false;
        time = 0f;
        firstActive = true;
    }

    public void activateRestart()
    {
        if(!isActive)
        {
            isActive = true;
            isRestart = true;
        }
    }

    public void activateNextGame()
    {
        if(!isActive)
        {
            isActive = true;
            isRestart = false;
        }

    }
}
