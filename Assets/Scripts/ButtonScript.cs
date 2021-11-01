using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject CreditsPannel;

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnCreditButtonPressed()
    {
        CreditsPannel.SetActive(true);
    }

    public void OnCreditBackButtonPressed()
    {
        CreditsPannel.SetActive(false);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
