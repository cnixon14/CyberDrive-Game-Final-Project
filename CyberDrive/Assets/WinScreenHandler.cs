using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenHandler : MonoBehaviour
{
    public GameObject winUI;
    public static bool hasWon = false;
    private bool isActive = false;

    private void Start()
    {
        winUI.SetActive(false);
    }

    private void Update()
    {
        if (hasWon == true && isActive == false)
        {
            winMenu();
            isActive = true;
        }
    }

    void winMenu()
    {
        winUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level One");
        winUI.SetActive(false);
        isActive = false;
        hasWon = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
