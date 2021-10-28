using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinningGame : MonoBehaviour
{
    //private AudioSource game;

    private void Start()
    {
     //   game = GetComponent<AudioSource>();
    }
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
       // game.Stop();
        SceneManager.LoadScene("MainMenu");

    }
    private void Update()
    {
        //game.Play();
    }
}

