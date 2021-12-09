using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerBattleCharacter : ICharacter
{
    private AICharacter opponent;
    private EncounterInstance myEncounter;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GameObject buttonArea;

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

        // Load Player's health
        LoadHealth();

        // Load Player's abilities
        LoadAbilities();
        
    }

    private void LoadAbilities()
    {
        // Get the Abilities
        abilities = BattleSceneHandler.playerAbilities;

        // Load the abilities into buttons
        for (int i = 0; i < abilities.Count; i++)
        {
            // Create a new Button, add to button area
            var abilityButton = Instantiate(buttonPrefab, buttonArea.transform);

            // Rename button, change on click event
            abilityButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = abilities[i].name;

            int index = i;
            abilityButton.GetComponent<Button>().onClick.AddListener(delegate { UseAbility(index); });

        }
    }

    public override void TakeTurn(EncounterInstance encounter)
    {
        opponent = encounter.Enemy;
        myEncounter = encounter;
        Debug.Log("Player Taking turn");
    }

    public void UseAbility(int slot)
    {
        // Check if ability has uses
        bool choseUseableAbility = true;

        // If using the uses and consumable slots from the ability script
        //if (abilities[slot].isConsumable && abilities[slot].usesRemaining <= 0)
        //{
        //    choseUseableAbility = false;
        //}

        // This is using the static ints found in the PlayerBehaviour Script
        if (abilities[slot].name == "PowerBeam" && PlayerBehaviour.powerUse <= 0)
        {
            choseUseableAbility = false;
        }
        else if (abilities[slot].name == "EatFood" && PlayerBehaviour.healUse <= 0)
        {
            choseUseableAbility = false;
        }
        else if (abilities[slot].name == "AuraShield" && PlayerBehaviour.shieldUse <= 0)
        {
            choseUseableAbility = false;
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
