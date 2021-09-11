using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject _canvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                UnpauseGame(); 
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        _canvas.SetActive(true);
        isPaused = !isPaused;

    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        _canvas.SetActive(false);
        isPaused = !isPaused;
    }
}
