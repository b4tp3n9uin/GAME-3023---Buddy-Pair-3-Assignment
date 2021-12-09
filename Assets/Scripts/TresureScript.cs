using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureScript : MonoBehaviour
{
    bool Open;
    public int upgradeValue;
    public Animator TresAnimator;

    const int MAX_ATK_N_HEAL = 30, // Maximum Value for Small attack and Heal.
        MAX_POWER_VAL = 40; // Maximum Value for Large Attack.

    // This Text will animate and show your Upgrades.
    [Header("Text Animation")]
    public TMPro.TextMeshProUGUI UpgradeText;
    public Animator TextAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Open = false;
        TresAnimator.SetBool("IsOpen", Open);

        UpgradeText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Open && PlayerBehaviour.keys > 0) // If tresure chest is not unlocked and you have keys, open it.
            {
                // When you interact with the Tresure Chest, you get an upgrade on Your Powerups and uses.
                Open = true;
                PlayerBehaviour.keys--;
                Debug.Log("Keys: " + PlayerBehaviour.keys);
                FindObjectOfType<AudioManager>().Play("Treasure");
                UpgradeAbilities();
                TresAnimator.SetBool("IsOpen", Open);
            }
            else if (!Open && PlayerBehaviour.keys == 0)
            {
                FindObjectOfType<AudioManager>().Play("Error");
                DisplayUpdateText("                     Out of KEYS!");
            }
        }
    }

    void UpgradeAbilities()
    {
        // Function for upgrading one of your abilities and and adding more use to one of the abilities
        int selected_Abty = Random.Range(1, 5);
        int add = Random.Range(1, 4);

        if (selected_Abty == 1)
        {
            // Upgrade Small Attack
            SmallAtkUpgrade();
        }
        else if (selected_Abty == 2)
        {
            // Upgrade spinach heal and add more uses
            HealEatUpgrade(add);
        }
        else if (selected_Abty == 3)
        {
            // Upgrade Power Attack and add more uses.
            LargeAtkUpgrade(add);
        }
        else if (selected_Abty == 4)
        {
            // Add more uses to Aura Shield.
            AddAuraShield(add);
        }
        else
        {
            LargeAtkUpgrade(add);
        }
    }

    void SmallAtkUpgrade() // Upgrade Small Attack
    {
        if (PlayerBehaviour.smlAtk_value < MAX_ATK_N_HEAL) // Small Attack cannot surpass a value over 30
            PlayerBehaviour.smlAtk_value += 5;

        DisplayUpdateText("Upgraded the Power of the Sword!");
    }

    void LargeAtkUpgrade(int use) // Upgrade Power Attack and add more uses.
    {
        if (PlayerBehaviour.lrgAtk_value < MAX_POWER_VAL) // Large Attack cannot surpass a value over 40
            PlayerBehaviour.lrgAtk_value += 5;

        PlayerBehaviour.powerUse += use;
        DisplayUpdateText("Upgrade on Power Beam with " + use + " additional uses");
    }

    void HealEatUpgrade(int use) // Upgrade spinach heal and add more uses
    {
        if (PlayerBehaviour.heal_value < MAX_ATK_N_HEAL) // Heal cannot surpass a value over 30
            PlayerBehaviour.heal_value += 5;

        PlayerBehaviour.healUse += use;
        DisplayUpdateText("Upgrade on spinach Heal with " + use + " additional uses");
    }

    void AddAuraShield(int use) // Add more uses to Aura Shield.
    {
        PlayerBehaviour.shieldUse += use;
        DisplayUpdateText(use + " additional uses for Aura Shield");
    }

    void DisplayUpdateText(string msg)
    {
        // Display the Upgrade and additional use on ability.
        UpgradeText.text = msg;
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        // Animate Upgrade text
        TextAnimator.SetBool("IsClaimed", true);
        yield return new WaitForSeconds(3.0f);
        UpgradeText.text = "";
        TextAnimator.SetBool("IsClaimed", false);
    }

}
