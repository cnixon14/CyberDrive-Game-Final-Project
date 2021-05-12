using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenHandler : MonoBehaviour
{
    public GameObject deathUI;
    public static bool isDead = false;
    private bool isActive = false;

    private void Start()
    {
        deathUI.SetActive(false);
    }

    private void Update()
    {
        if (isDead == true && isActive == false)
        {
            deathMenu();
            isActive = true;
        }
    }

    void deathMenu()
    {
        deathUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level One");
        deathUI.SetActive(false);
        isActive = false;
        isDead = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
