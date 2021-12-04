using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "AbilitySystem/Shield")]
public class IShieldEffect : IEffect
{
    public override void Apply(ICharacter self, ICharacter target)
    {
        PlayerBehaviour.isProtected = true;
        PlayerBehaviour.shieldUse--;
    }
}
