using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public HighScores m_HighScores;

    public Text m_MessageText;
    public Text m_TimerText;

    public GameObject[] m_Tanks;

    public GameObject m_HighScorePanel;
    public Text m_HighScoresText;

    public Button m_NewGameButton;
    public Button m_HighScoresButton;

    public Button m_MenuButton;

    private float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }


    //Defining Gamestates
    public enum GameState
    {

        Start,
        Playing,
        GameOver

    };

    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }


    //Upon the scene loading set gamestate to playing (Switch case = 0)
    private void Awake()
    {
        m_GameState = GameState.Start;
    }

    //Initialises the scene for the player, Displays the 'Get Ready' text on screen. This Line is called any time the scene is loaded

    private void Start()
    {

        for (int i = 0; i < m_Tanks.Length; i++)
        {

            m_Tanks[i].SetActive(false);

        }

        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready";

        m_HighScorePanel.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        m_MenuButton.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
        //Game states are given values 0 - 2, Checks each value and runs code

        switch (m_GameState)
        {

            
            //Game state = 0/start. Sets all tanks and UI to visible after a response from the player (either Lmouse or Enter key)
            case GameState.Start:

                if (Input.GetKeyUp(KeyCode.Return) == true || Input.GetKeyUp(KeyCode.Mouse0) == true)
                {

                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";

                    //Set game state to 1
                    m_GameState =GameState.Playing;

                    for (int i = 0; i < m_Tanks.Length; i++)
                    {

                        m_Tanks[i].SetActive(true);

                    }

                }

                break;


            //Gamestate = 1/Playing. While the game runs have the timer continuously tick. Format text in UI to 00:00
            case GameState.Playing:

                bool isGameOver = false;

                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);

                m_TimerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));


                //Checks if only the player is left
                if (OneTankLeft() == true)
                {

                    isGameOver = true;

                }

                //Checks if the player is dead
                else if (IsPlayerDead() == true)
                {

                    isGameOver = true;

                }

                if (isGameOver == true)
                {

                    //Code that determines what happens when the game ends with the player either winning or losing


                    //When the game enters the gamestate GameOver/2 it will turn the timer invisible and bring up the UI
                    m_GameState = GameState.GameOver;

                    m_TimerText.gameObject.SetActive(false);

                    m_NewGameButton.gameObject.SetActive(true);
                    m_HighScoresButton.gameObject.SetActive(true);
                    m_MenuButton.gameObject.SetActive(true);

                    if (IsPlayerDead() == true)
                    {

                        //Displays the text on the screen to tell the player they lost and sends a message to the console to make sure the game registers the code

                        m_MessageText.text = "TRY AGAIN";
                        Debug.Log("Player lost the game");


                    }
                    else
                    {

                        //Displays winning test and sends message to console, then saves the time on the time to the txt file 

                        m_MessageText.text = "WINNER!!!";

                        Debug.Log("Player won the game");

                        m_HighScores.AddScore(Mathf.RoundToInt(m_gameTime));
                        m_HighScores.SaveScoresToFile();

                    }

                }

                break;

            case GameState.GameOver:

                if (Input.GetKeyUp(KeyCode.Return) == true)
                {

                    /*m_gameTime = 0;
                    m_GameState = GameState.Playing;

                    m_MessageText.text = "";
                    m_TimerText.gameObject.SetActive(true);

                    for (int i = 0; i < m_Tanks.Length; i++)
                    {

                        m_Tanks[i].SetActive(true);

                    }*/

                    //Old code that restarted the game if the user hit return, changed to code below for a more consistant fix

                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                }

                break;

        }

        //Exits program with the escape key

        if (Input.GetKeyUp(KeyCode.Escape))
        {

            Application.Quit();

        }
        
    }

    //Bool function to return true if the player is the only tank left
    private bool OneTankLeft()
    {

        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {

            if (m_Tanks[i].activeSelf == true)
            {

                numTanksLeft++;

            }

        }

        return numTanksLeft <= 1;

    }

    //Bool function to return true if the player dies
    private bool IsPlayerDead()
    {

        for (int i = 0; i < m_Tanks.Length; i++)
        {

            if (m_Tanks[i].activeSelf == false)
            {

                if (m_Tanks[i].tag == "Player")
                    return true;

            }

        }

        return false;

    }

    //Restarts the game for the player

    public void OnNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        /*m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.SetActive(false);

        m_gameTime = 0;
        m_GameState = GameState.Playing;
        m_TimerText.gameObject.SetActive(true);
        m_MessageText.text = "";

        m_Tanks[0].gameObject.transform.position = Vector3.zero;
        //m_Tanks[0].gameObject

        for (int i = 0; i < m_Tanks.Length; i++)
        {

            m_Tanks[i].SetActive(true);

        }*/

    }

    //Calls the data from the txt file when the 'Highscores' button is pressed, displays it in the UI
    public void OnHighScores()
    {

        m_MessageText.text = "";

        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.SetActive(true);

        string text = "";
        for (int i = 0; i < m_HighScores.scores.Length; i++)
        {

            int seconds = m_HighScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));

        }

        m_HighScoresText.text = text;

    }

    //Sends player to the menu scene

    public void OnStartMenu()
    {

        SceneManager.LoadScene("MenuScene");

    }

}
