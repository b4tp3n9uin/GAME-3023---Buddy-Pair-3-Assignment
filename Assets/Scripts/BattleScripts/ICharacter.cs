using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ICharacter : MonoBehaviour
{
    [Header("Health")]
    protected float currentHealth;

    public float Health
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;

            if (currentHealth < 0)
            {
                currentHealth = 0.0f;
            }

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    public float HealthPercent
    {
        get { return currentHealth / maxHealth; }
    }

    [SerializeField]
    protected float maxHealth;

    [SerializeField]
    protected Slider hpBar;

    public Slider HPBar
    {
        get { return hpBar; }
    }

    [Header("Abilities")]
    [SerializeField]
    protected Ability[] abilities;
    [SerializeField]
    [Range(0.1f, 10.0f)]
    protected float attack;

    public float Attack
    {
        get { return attack; }
    }

    public abstract void TakeTurn(EncounterInstance encounter);

    public void InitHealth()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        InitHealth();
    }
}
