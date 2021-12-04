using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    PLAYER_Small = 0,
    PLAYER_Large = 1,
    ENEMY = 3
}

// Right click -> Create
[CreateAssetMenu(fileName = "Damage", menuName = "AbilitySystem/Damage")]
public class IDamageEffect : IEffect
{
    public float damageAmount;
    public AttackType attack;

    

    public override void Apply(ICharacter self, ICharacter target)
    {
        if (attack == AttackType.PLAYER_Small)
        {
            damageAmount = PlayerBehaviour.smlAtk_value;
            target.Health -= (self.Attack * damageAmount);
        }
        else if (attack == AttackType.PLAYER_Large)
        {
            damageAmount = PlayerBehaviour.lrgAtk_value;
            PlayerBehaviour.powerUse--;
            target.Health -= (self.Attack * damageAmount);

        }
        else if (attack == AttackType.ENEMY)
        {
            if (PlayerBehaviour.isProtected)
            {
                Debug.Log("Protected");
                PlayerBehaviour.isProtected = false;
            }
            else
            {
                target.Health -= (self.Attack * damageAmount);
            }
        }
    }
}


