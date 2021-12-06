using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EncounterInstance : MonoBehaviour
{
    [SerializeField]
    private PlayerBattleCharacter player;

    public PlayerBattleCharacter Player
    { 
        get { return player; }
        private set { player = value; }
    }

    [SerializeField]
    private AICharacter enemy;
    public AICharacter Enemy
    {
        get { return enemy; }
        private set { enemy = value; }
    }

    [SerializeField]
    private float abilityTime = 4.0f;

    // Events
    public UnityEvent<ICharacter> onCharacterTurnBegin;
    public UnityEvent<ICharacter> onCharacterTurnEnd;
    public UnityEvent<PlayerBattleCharacter> onPlayerTurnBegin;
    public UnityEvent<PlayerBattleCharacter> onPlayerTurnEnd;
    public UnityEvent<AICharacter> onEnemyTurnBegin;
    public UnityEvent<AICharacter> onEnemyTurnEnd;

    public UnityEvent<ICharacter, Ability> onCharacterAbilityUsed;
    public UnityEvent<ICharacter, ICharacter> onHPChange;
    public UnityEvent<Ability> onTriedAbilityOutOfUses;

    // Turn counter
    private int turnNumber = 0;

    // This is a reference to keep track of our coroutine
    private IEnumerator turnCoroutine = null;

    [SerializeField]
    private ICharacter currentCharacterTurn;

    // Start is called before the first frame update
    void Start()
    {
        // Set the player if its not already put in 
        if (player == null)
            player = FindObjectOfType<PlayerBattleCharacter>();

        // Randomize the Enemy if it is not already put in
        if (enemy == null)
            enemy = FindObjectOfType<AICharacter>();

        currentCharacterTurn = player;
        currentCharacterTurn.TakeTurn(this);
    }

    public void AdvanceTurns(Ability abilityUsed)
    {
        onCharacterTurnEnd.Invoke(currentCharacterTurn);

        // Run coroutine to take care of abilities
        if (turnCoroutine != null)
        {
            StopCoroutine(turnCoroutine);
        }

        turnCoroutine = HandleTurn(abilityUsed);
        StartCoroutine(turnCoroutine);

        turnNumber++;
    }

    public void UsedAbiliyOutOfUses(Ability abilityUsed)
    {
        onTriedAbilityOutOfUses.Invoke(abilityUsed);
    }

    public void UpdateHealthBars()
    {
        onHPChange.Invoke(Player, Enemy);
    }

    IEnumerator HandleTurn(Ability abilityUsed)
    {
        // Character uses ability
        onCharacterAbilityUsed.Invoke(currentCharacterTurn, abilityUsed);

        // Disable player's UI immediately after selecting ability
        if (currentCharacterTurn == player)
        {
            onPlayerTurnEnd.Invoke(player);
            currentCharacterTurn = enemy;

            // Wait for ability time
            yield return new WaitForSeconds(abilityTime);
        }
        // Set the player's UI to active here after the enemy plays their animation
        else
        {
            // Wait for ability time
            yield return new WaitForSeconds(abilityTime);

            currentCharacterTurn = player;
            onPlayerTurnBegin.Invoke(player);
        }

        // Change HP bars
        UpdateHealthBars();

        if (player.Health <= 0) // Game Over.
        {
            player.Health = 100;
            player.SaveHealth();
            SceneManager.LoadScene("GameOverScene");
        }

        if (enemy.Health <= 0) // Win Battle.
        {
            player.SaveHealth();

            if (PlayerBehaviour.bosses == 0)
                SceneManager.LoadScene("WinScene");
            else
            {
                PlayerBehaviour.keys += Random.RandomRange(1, 3);
                SceneManager.LoadScene("SampleScene");
            }
            
        }

        // Set next character's turn
        onCharacterTurnBegin.Invoke(currentCharacterTurn);
        currentCharacterTurn.TakeTurn(this);
    }
}
