using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AICharacter : ICharacter
{
    private PlayerBattleCharacter player;

    [SerializeField]
    private Animator animator;

    protected override void Start()
    {
        // call base start
        base.Start();

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

    private Ability ChooseMove()
    {
        // Create weighting for abilities
        float[] weights = new float[4];
        float[] thresholds = new float[4];

        for (int i = 0; i < abilities.Length; i++)
        {
            weights[i] = 0.25f;
        }

        // decide ability to use

        var chosenAbility = abilities[0];

        return chosenAbility;

    }
}
