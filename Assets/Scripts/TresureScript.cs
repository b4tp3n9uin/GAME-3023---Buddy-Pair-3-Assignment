using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureScript : MonoBehaviour
{
    bool Open;
    public int upgradeValue;
    public Animator TresAnimator;

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

    // Update is called once per frame
    void Update()
    {
        
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
                DisplayUpdateText("                     Out of KEYS!");
            }
        }
    }

    void UpgradeAbilities()
    {
        // Function for upgrading one of your abilities and and adding more use to one of the abilities
        int selected_Abty = Random.Range(1, 4);
        int add = Random.Range(1, 3);

        if (selected_Abty == 1)
        {
            // Upgrade Small Attack
            PlayerBehaviour.smlAtk_value += 5;
            DisplayUpdateText("Upgraded the Power of the Sword!");
        }
        else if (selected_Abty == 2)
        {
            // Upgrade spinach heal and add more uses
            PlayerBehaviour.healUse += add;
            PlayerBehaviour.heal_value += 5;
            DisplayUpdateText("Upgrade on spinach Heal with " + add + " additional uses");
        }
        else if (selected_Abty == 3)
        {
            // Upgrade Power Attack and add more uses.
            PlayerBehaviour.powerUse += add;
            PlayerBehaviour.lrgAtk_value += 5;
            DisplayUpdateText("Upgrade on Power Beam with " + add + " additional uses");
        }
        else if (selected_Abty == 4)
        {
            // Upgrade Aura Shield and add more uses.
            PlayerBehaviour.shieldUse += add;
            DisplayUpdateText("Upgrade on Aura with " + add + " additional uses");
        }
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
