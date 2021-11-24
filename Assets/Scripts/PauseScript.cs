using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject PausePannel;
    public PlayerBehaviour player;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        player = FindObjectOfType<PlayerBehaviour>();
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

    public void OnSavePressed()
    {
        //Save Player Location.
        player.SavePlayerLocation();
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
}
