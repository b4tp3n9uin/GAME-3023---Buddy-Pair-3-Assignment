using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject PausePannel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PausePannel.SetActive(false);
    }

    // Function for the Pause button, weather to pause or unpause
    public void OnPausePressed()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            PausePannel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PausePannel.SetActive(false);
        }
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
}
