using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Right click -> Create
[CreateAssetMenu(fileName = "Damage", menuName = "AbilitySystem/Damage")]
public class IDamageEffect : IEffect
{
    public float damageAmount;

    public override void Apply(ICharacter self, ICharacter target)
    {
        target.Health -= (self.Attack * damageAmount);
    }
}
