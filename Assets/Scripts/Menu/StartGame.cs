using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public GameObject m_StartMenu;
    public Button m_StartButton;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        


    }


    //Changes to the scene with the Tanks game in it. Hense starts the game
    public void OnStartGame()
    {

        SceneManager.LoadScene("SampleScene");

    }


    //Exits the game
    public void OnExitGame()
    {

        Application.Quit();

    }

}
