using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ConsumableAbilities
{
    PowerBeam = 0,
    Heal = 2,
    Shield = 3
}

public class PlayerBattleCharacter : ICharacter
{
    private AICharacter opponent;
    private EncounterInstance myEncounter;

    [SerializeField]
    private Animator animator;

    public string overworldScene = "GameScene";

    private string playerHealthSaveKey = "PlayerHealth";

    protected override void Awake()
    {
        var list = FindObjectsOfType<Slider>();
        foreach (var item in list)
        {
            if (item.name == "PlayerHPSlider")
                hpBar = item;
        }

        LoadHealth();
    }

    public override void TakeTurn(EncounterInstance encounter)
    {
        opponent = encounter.Enemy;
        myEncounter = encounter;
        Debug.Log("Player Taking turn");

        encounter.UpdateHealthBars();
    }

    public void UseAbility(int slot)
    {
        // Check if ability has uses
        bool choseUseableAbility = true;

        switch (slot)
        {
            case (int)ConsumableAbilities.PowerBeam:

                if (PlayerBehaviour.powerUse <= 0)
                {
                    Debug.Log("No More Power Use");
                    choseUseableAbility = false;
                }

                break;
            case (int)ConsumableAbilities.Heal:

                if (PlayerBehaviour.healUse <= 0)
                {
                    Debug.Log("Out of Heal Use!");
                    choseUseableAbility = false;
                }
                break;
            case (int)ConsumableAbilities.Shield:

                if (PlayerBehaviour.shieldUse <= 0)
                {
                    Debug.Log("Out of Shield Use!");
                    choseUseableAbility = false;
                }
                break;
            default:
                break;
        }

        // If the chosen ability has uses
        if (choseUseableAbility)
        {
            // Use the ability in the slot
            abilities[slot].Cast(this, opponent);

            // Play the animation for that ability
            animator.SetTrigger(abilities[slot].name);

            // End turn
            myEncounter.AdvanceTurns(abilities[slot]);
        }
        else
        // If the chosen ability does NOT have uses
        {
            // Display to text window that the ability does not have uses, continue player's turn
            myEncounter.UsedAbiliyOutOfUses(abilities[slot]);
        }

    }

    public void SaveHealth()
    {
        // Save the Player's health
        string saveHealth = Health + "";

        PlayerPrefs.SetString(playerHealthSaveKey, saveHealth);
    }

    public void LoadHealth()
    {
        // Check if the key exists...
        if (!PlayerPrefs.HasKey(playerHealthSaveKey))
        {
            return;
        }

        // Get saved health
        string saveHealth = PlayerPrefs.GetString(playerHealthSaveKey, "");

        // Set Player's health
        Health = float.Parse(saveHealth);
    }

    public void Flee()
    {
        // Save Health
        SaveHealth();

        // Load the overworld scene
        SceneManager.LoadScene(overworldScene);
    }
}
