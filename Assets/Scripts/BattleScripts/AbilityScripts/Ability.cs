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
    private IEffect[] effects;

    public void Cast(ICharacter self, ICharacter other)
    {
        foreach (IEffect effect in effects)
        {
            effect.Apply(self, other);
        }
    }
}
