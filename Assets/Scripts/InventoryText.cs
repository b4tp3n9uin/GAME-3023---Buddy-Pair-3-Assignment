using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryText : MonoBehaviour
{
    // Class to show the Inventory Text.

    public TMPro.TextMeshProUGUI InventoryCount;

    // Update is called once per frame
    void Update()
    {
        displayInventory();
    }

    void displayInventory()
    {
        InventoryCount.text = "\nSpinach: " + PlayerBehaviour.healUse +
            "\nAura Use: " + PlayerBehaviour.shieldUse +
            "\nPower Use: " + PlayerBehaviour.powerUse;
    }
}
