using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AICharacter : ICharacter
{
    protected PlayerBattleCharacter player;

    [SerializeField]
    protected Animator animator;

    protected override void Awake()
    {
        var list = FindObjectsOfType<Slider>();
        foreach (var item in list)
        {
            if (item.name == "EnemyHPSlider")
                hpBar = item;
        }

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public override void TakeTurn(EncounterInstance encounter)
    {
        player = encounter.Player;

        StartCoroutine(DelayDecision(encounter));
    }

    IEnumerator DelayDecision(EncounterInstance encounter)
    {
        // Choose what action to do
        Ability ability = ChooseMove();

        Debug.Log("Enemy Taking turn");
        yield return new WaitForSeconds(3.0f);

        // Use the ability in the slot
        ability.Cast(this, player);

        // Play the animation for that ability
        animator.SetTrigger(ability.name);
        
        encounter.AdvanceTurns(ability);
    }

    protected virtual Ability ChooseMove()
    {
        // Default Enemy Behaviour (If not using custom enemy script)

        // Create weighting for abilities
        float[] weights = new float[4];

        for (int i = 0; i < abilities.Count; i++)
        {
            weights[i] = 0.25f;
        }

        int randSelector = Random.Range(0, abilities.Count);
        var chosenAbility = abilities[randSelector];

        // decide ability to use
        return chosenAbility;
    }
}
