using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfiguration : MonoBehaviour
{
    public enum Difficult { EASY, MEDIUM, HARD };
    public enum GameType { MULTIPLAYER, SINGLEPLAYER };

    public bool isPlayer1Bot = true;
    public bool isPlayer2Bot = true;
    public Difficult bot1Difficult = Difficult.MEDIUM;
    public Difficult bot2Difficult = Difficult.MEDIUM;
    public float globalVolume = 1;
    public GameType gameType;

    public static GameConfiguration gameConfiguration;

    void Awake()
    {
        if(gameConfiguration == null)
        {
            gameConfiguration = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void easyGameConfiguration()
    {
        isPlayer1Bot = false;
        isPlayer2Bot = true;
        bot2Difficult = Difficult.EASY;
        gameType = GameType.SINGLEPLAYER;
    }

    public void mediumGameConfiguration()
    {
        isPlayer1Bot = false;
        isPlayer2Bot = true;
        bot2Difficult = Difficult.MEDIUM;
        gameType = GameType.SINGLEPLAYER;
    }

    public void hardGameConfiguration()
    {
        isPlayer1Bot = false;
        isPlayer2Bot = true;
        bot2Difficult = Difficult.HARD;
        gameType = GameType.SINGLEPLAYER;
    }

    public void multiplayerGameConfiguration()
    {
        isPlayer1Bot = false;
        isPlayer2Bot = false;
        gameType = GameType.MULTIPLAYER;
    }
}
