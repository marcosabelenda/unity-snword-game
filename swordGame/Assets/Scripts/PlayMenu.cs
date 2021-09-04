using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public void PlaySingleplayer()
    {
        GameConfiguration.gameConfiguration.easyGameConfiguration();
        SceneManager.LoadScene("Tower");
    }

    public void PlayGameSingleplayerEasy()
    {
        GameConfiguration.gameConfiguration.easyGameConfiguration();
        SceneManager.LoadScene("Game");

    }

    public void PlayGameSingleplayerMedium()
    {
        GameConfiguration.gameConfiguration.mediumGameConfiguration();
        SceneManager.LoadScene("Game");

    }

    public void PlayGameSingleplayerHard()
    {
        GameConfiguration.gameConfiguration.hardGameConfiguration();
        SceneManager.LoadScene("Game");

    }

    public void PlayGameMultiplayer()
    {
        GameConfiguration.gameConfiguration.multiplayerGameConfiguration();
        SceneManager.LoadScene("Game");
    }
}
