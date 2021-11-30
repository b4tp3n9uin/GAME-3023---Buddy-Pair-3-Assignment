using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Right click -> Create
[CreateAssetMenu(fileName = "Heal", menuName = "AbilitySystem/Heal")]
public class IHealEffect : IEffect
{
    public float healAmount;

    public override void Apply(ICharacter self, ICharacter target)
    {
        self.Health += healAmount;
    }
}
