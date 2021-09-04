using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountdownTimer : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime = 60f;
    private bool stopTimer = false;
    public GameObject player1;
    public GameObject player2;
    public GameObject player1WinUI;
    public GameObject player2WinUI;
    public GameObject draw;
    public GameObject canvas;

    [SerializeField] private Text countdownText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        countdownText.color = Color.black;   
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopTimer)
        {
            if (player1.GetComponent<Core>().oponentLife > 0f && player2.GetComponent<Core>().oponentLife > 0f)
                currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 0)
            {
                currentTime = 0;
                //Time.timeScale = 0f;
                stopTimer = true;
            }

            if (currentTime <= 20 && currentTime > 5)
            {
                countdownText.color = new Color(1f, 0.75f, 0.016f, 1f);
            }

            if (currentTime <= 5)
            {
                countdownText.color = new Color(0.8f, 0f, 0f, 1f);
            }
        }
        else
        {
            if (player1.GetComponent<Core>().oponentLife > player2.GetComponent<Core>().oponentLife)
            {
                player1.GetComponent<PlayerController>().coreLogic.resetTriggers();
                player2.GetComponent<PlayerController>().coreLogic.resetTriggers();
                player1.GetComponent<PlayerController>().coreLogic.setBool("dance", true);
                player1.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.DANCE;
                player2.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.END_GAME;
                player2.GetComponent<PlayerController>().coreLogic.areAction = true;
                player1WinUI.SetActive(true);
                if (GameConfiguration.gameConfiguration.gameType == GameConfiguration.GameType.SINGLEPLAYER)
                {
                    canvas.GetComponent<NewGame>().activateNextGame();
                }
                else
                {
                    canvas.GetComponent<NewGame>().activateRestart();
                }
            }

            else if (player2.GetComponent<Core>().oponentLife > player1.GetComponent<Core>().oponentLife)
            {
                player1.GetComponent<PlayerController>().coreLogic.resetTriggers();
                player2.GetComponent<PlayerController>().coreLogic.resetTriggers();
                player2.GetComponent<PlayerController>().coreLogic.setBool("dance", true);
                player2.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.DANCE;
                player1.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.END_GAME;
                player1.GetComponent<PlayerController>().coreLogic.areAction = true;
                player2WinUI.SetActive(true);
                canvas.GetComponent<NewGame>().activateRestart();

            }
            else if (player2.GetComponent<Core>().oponentLife == player1.GetComponent<Core>().oponentLife)
            {

                draw.SetActive(true);
                player1.GetComponent<PlayerController>().coreLogic.resetTriggers();
                player2.GetComponent<PlayerController>().coreLogic.resetTriggers();
                player1.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.END_GAME;
                player1.GetComponent<PlayerController>().coreLogic.areAction = true;
                player2.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.END_GAME;
                player2.GetComponent<PlayerController>().coreLogic.areAction = true;
                canvas.GetComponent<NewGame>().activateRestart();
            }
        }
    
    }

   
}
    
    

