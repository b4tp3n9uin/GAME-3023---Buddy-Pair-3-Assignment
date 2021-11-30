using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBattleCharacter : ICharacter
{
    private AICharacter opponent;
    private EncounterInstance myEncounter;

    [SerializeField]
    private Animator animator;

    public string overworldScene = "GameScene";

    protected void Start()
    {
        // call base start
        base.Start();

        var list = FindObjectsOfType<Slider>();
        foreach (var item in list)
        {
            if (item.name == "PlayerHPSlider")
                hpBar = item;
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
        // Use the ability in the slot
        abilities[slot].Cast(this, opponent);

        // Play the animation for that ability
        animator.SetTrigger(abilities[slot].name);

        myEncounter.AdvanceTurns(abilities[slot]);
    }

    public void Flee()
    {
        // Load the overworld scene
        SceneManager.LoadScene(overworldScene);
    }
}
