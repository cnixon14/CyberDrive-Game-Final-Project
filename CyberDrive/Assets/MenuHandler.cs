using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject MenuGUI;

    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("Level One");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
