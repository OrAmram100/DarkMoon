using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void Setup()
    {
        Cursor.lockState = CursorLockMode.Confined;
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        Debug.Log("hi");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
