using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullBehaviour : AICharacter
{
    protected override Ability ChooseMove()
    {
        // Include Specific Enemy Behaviour here

        // Create weighting for abilities
        float[] weights = new float[abilities.Count];
        float[] thresholds = new float[abilities.Count];

        // Set the Weights for each ability here, can change weight based
        // on circumstances, like health left, player health, or whatever else.
        // NOTE: You will need to know if there are abilities corresponding to your input index

        // Default Weights, even
        for (int i = 0; i < abilities.Count; i++)
        {
            weights[i] = 0.25f;
        }

        // If the player's health is less than half, less likely to use more powerful attack
        if (player.HealthPercent < 0.5f)
        {
            weights[0] = 0.6f;
            weights[1] = 0.2f;
            weights[2] = 0.1f;
        }

        // If our Bull's Health is above 80, cannot use heal
        if (currentHealth > 80)
        {
            weights[2] = 0.0f;
        }

        // Add together weights to get maximum weight
        float weightedMax = 0.0f;

        for (int i = 0; i < abilities.Count; i++)
        {
            weightedMax += weights[i];
            thresholds[i] = weightedMax;
        }

        // Ability is chosen by which threshold the weightedSelector is under
        Ability chosenAbility = abilities[0];
        float weightedSelector = Random.Range(0, weightedMax);

        // Go through thresholds to see which ability was chosen
        for (int i = 0; i < abilities.Count; i++)
        {
            if (weightedSelector <= thresholds[i])
            {
                // Found the chosen ability
                // The first threshold it is under
                chosenAbility = abilities[i];
                break;
            }
        }

        return chosenAbility;
    }
}
