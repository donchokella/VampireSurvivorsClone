using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuFirstButton;

    public string firstLevelName;

    public InputManager inputManager;

    private void Awake()
    {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);

    }

    private void Update()
    {
        if(Input.GetButton("SubmitGamePad"))
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        inputManager.GetInputs();
        SceneManager.LoadScene(firstLevelName);

        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
