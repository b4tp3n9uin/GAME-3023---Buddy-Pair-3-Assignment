using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthType
{
    PLAYER = 0,
    ENEMY = 1
}

// Right click -> Create
[CreateAssetMenu(fileName = "Heal", menuName = "AbilitySystem/Heal")]
public class IHealEffect : IEffect
{
    public float healAmount;
    public HealthType healthType;

    public override void Apply(ICharacter self, ICharacter target)
    {
        if (healthType == HealthType.PLAYER)
        {
            healAmount = PlayerBehaviour.heal_value;
            PlayerBehaviour.healUse--;
            self.Health += healAmount;
        }
        else if (healthType == HealthType.ENEMY)
        {
            self.Health += healAmount;
            PlayerBehaviour.isProtected = false;
        }
    }
}
