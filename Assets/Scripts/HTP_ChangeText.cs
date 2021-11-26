using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTP_ChangeText : MonoBehaviour
{
    //Script to switch the text in the How To Play Pannel in the Main Menu.

    public GameObject WorldText, BattleText;


    // Start is called before the first frame update
    void Start()
    {
        // Display Instructions for the overworld Level at start.
        ActivateText(true);
    }

    public void OnWorldButtonPressed()
    {
        // Display Instructions for the overworld Level.
        ActivateText(true);
    }

    public void OnBattleButtonPressed()
    {
        // Display Instructions for the Battle.
        ActivateText(false);
    }

    void ActivateText(bool flip)
    {
        // function that will change the text for instruction of Battle/Overworld level.
        WorldText.SetActive(flip);
        BattleText.SetActive(!flip);
    }
}
