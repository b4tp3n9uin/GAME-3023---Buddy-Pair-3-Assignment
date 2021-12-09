using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class EncounterInstance : MonoBehaviour
{
    [SerializeField]
    private PlayerBattleCharacter player;

    public PlayerBattleCharacter Player
    { 
        get { return player; }
        private set { player = value; }
    }

    [Header("Encounter Information")]
    [SerializeField]
    private Transform EnemySpawnPosition;
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private TextMeshProUGUI enemyNametext;

    private GameObject enemyCharacter;

    private AICharacter enemyAI;
    public AICharacter Enemy
    {
        get { return enemyAI; }
        private set { enemyAI = value; }
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
    public UnityEvent<ICharacter> onWinBattle;
    
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

        // Set Background Image
        backgroundImage.sprite = BattleSceneHandler.backgroundImage;

        // Set Enemy Character to enemy passed in from Random Encounter Script
        enemyCharacter = Instantiate(BattleSceneHandler.enemyCharacter, EnemySpawnPosition);
        enemyCharacter.name = BattleSceneHandler.enemyCharacter.name;
        enemyAI = enemyCharacter.GetComponent<AICharacter>();

        // Set Name Card
        enemyNametext.text = enemyCharacter.name;

        // Update initial health and set to player's turn.
        UpdateHealthBars();
        currentCharacterTurn = player;
        currentCharacterTurn.TakeTurn(this);

        // Play correct music
        musicSource.Stop();
        musicSource.clip = BattleSceneHandler.musicToPlay;
        musicSource.Play();
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
            currentCharacterTurn = enemyAI;

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

        if (enemyAI.Health <= 0) // Win Battle.
        {
            // Save Player's Health
            player.SaveHealth();

            // Trigger on Win battle event
            onWinBattle.Invoke(enemyAI);
        }
        else
        {
            // Set next character's turn
            onCharacterTurnBegin.Invoke(currentCharacterTurn);
            currentCharacterTurn.TakeTurn(this);
        }
    }
}
