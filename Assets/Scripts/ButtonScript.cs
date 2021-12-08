using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //Script for the buttons UI in the Game.

    public GameObject CreditsPannel;
    bool isInventoryActive;


    private string playerLocationSaveKey = "KaiLocation";

    void Start()
    {
        isInventoryActive = false;
    }

    public void OnNewGameButtonPressed()
    {
        ResetPlayerLocation();
        OnPlayButtonPressed();
    }

    public void OnPlayButtonPressed() // Press play
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnMainMenuButtonPressed() // Go to Main Menu
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void OnCreditButtonPressed() // Press Credits
    {
        CreditsPannel.SetActive(true);
    }

    public void OnHowToPlayButtonPressed() // Press Credits
    {
        CreditsPannel.SetActive(true);
    }

    public void OnHowToPlayBackButtonPressed() // Press Credits
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

    public void OnInvenotryPressed() // Function for the Inventory button and Pannel.
    {
        isInventoryActive = !isInventoryActive;

        if (isInventoryActive)
            CreditsPannel.SetActive(true);
        else
            CreditsPannel.SetActive(false);

    }

    public void ResetPlayerLocation()
    {
        // Save the Player's location (x, y, z)
        string playerLocation = "";

        playerLocation += (-1.0f) + ",";
        playerLocation += (0) + ",";
        playerLocation += (0) + ",";

        PlayerPrefs.SetString(playerLocationSaveKey, playerLocation);
    }

}
