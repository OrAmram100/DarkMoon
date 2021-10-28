using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour
{
    public void Setup()
    {
        Invoke("Delay", 2);
        Cursor.lockState = CursorLockMode.Confined;

    }

    public void Delay()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
