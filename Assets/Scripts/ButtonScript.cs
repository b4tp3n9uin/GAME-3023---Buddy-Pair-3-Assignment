using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject CreditsPannel;

    public void OnPlayButtonPressed() // Press play
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnCreditButtonPressed() // Press Credits
    {
        CreditsPannel.SetActive(true);
    }

    public void OnCreditBackButtonPressed() // Back button for Credits and Sign Menus.
    {
        Time.timeScale = 1.0f;
        CreditsPannel.SetActive(false);
    }

    public void OnExitButtonPressed() // Exit Game
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
