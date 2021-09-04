using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTowerScene : MonoBehaviour
{
    public static float MAX_TIME = 2.5f;
    public float time = 0.0f;
    public GameObject easy;
    public GameObject medium;
    public GameObject hard;


    void Start()
    {
        if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.EASY)
        {
            easy.SetActive(true);
            medium.SetActive(false);
            hard.SetActive(false);
        }
        else if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.MEDIUM)
        {
            easy.SetActive(false);
            medium.SetActive(true);
            hard.SetActive(false);
        }
        else if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.HARD)
        {
            easy.SetActive(false);
            medium.SetActive(false);
            hard.SetActive(true);
        }
    }

    void Update()
    {
        if(time < MAX_TIME)
        {
            time += Time.deltaTime;
        } else
        {
            SceneManager.LoadScene("Game");
        }
    }
}
