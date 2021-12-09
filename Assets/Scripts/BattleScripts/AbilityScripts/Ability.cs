using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Right click -> Create
[CreateAssetMenu(fileName = "NewAbility", menuName = "AbilitySystem/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private string description;

    [SerializeField]
    private bool consumable;

    public bool isConsumable
    {
        get
        {
            return consumable;
        }
    }

    [SerializeField]
    private int uses;

    public int usesRemaining
    {
        get
        {
            return uses;
        }
    }


    [SerializeField]
    private IEffect[] effects;

    public void Cast(ICharacter self, ICharacter other)
    {
        // If the ability either is NOT consumable or if it has uses
        if (!isConsumable || uses > 0)
        {
            if (isConsumable)
            {
                // Subtract from remaining uses
                uses--;
            }
            
            // Apply Effects
            foreach (IEffect effect in effects)
            {
                effect.Apply(self, other);
            }
        }
    }
}
