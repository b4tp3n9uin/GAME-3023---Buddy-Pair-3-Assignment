using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EncounterBattleBehaviour : MonoBehaviour
{
    [Header("Encounter Timer Settings")]
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float chanceOfEncounter = 0.2f;

    [SerializeField]
    private float tryEncounterTime = 0.5f;
    private float timeElapsed = 0.0f;

    [Header("Battle Scene Information")]
    [SerializeField]
    private Sprite backgroundImage;

    [SerializeField]
    private List<GameObject> encounterableEnemies;

    [SerializeField]
    private Vector2 playerSaveOffset = Vector2.zero;

    [SerializeField]
    private string battleScene = "BattleScene";

    [SerializeField]
    private AudioClip music;


    private PlayerBehaviour player;
    private bool playerCanEncounter = false;
    private bool encounterTimerDone = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is in the tall grass and they're moving
        // NOTE: if the player uses the rigidbody to move, use this check  (playerCanEncounter && player.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Mathf.Epsilon)
        if (playerCanEncounter && Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            TryEncounter();
        }
    }

    private void TryEncounter()
    {
        // If it's time to encounter an enemy
        if (encounterTimerDone)
        {
            float rand = Random.Range(0.0f, 1.0f);

            Debug.Log("You rolled a " + rand + ". Need below " + chanceOfEncounter + " for an encounter.");

            if (rand <= chanceOfEncounter)
            {
                // Save Player Location
                player.SavePlayerLocation(0.0f, 0.0f);

                // Load Battle Scene
                LoadBattleScene();

            }
            else
            {
                encounterTimerDone = false;
            }
        }
        else
        {
            // Increment Encounter Timer
            if((timeElapsed += Time.deltaTime) > tryEncounterTime)
            {
                encounterTimerDone = true;
                timeElapsed = 0.0f;
            }
        }
        
    }

    public void LoadBattleScene()
    {
        // Save Player's Location with an Offset
        player.SavePlayerLocation(playerSaveOffset.x, playerSaveOffset.y);

        // Set the background image
        BattleSceneHandler.backgroundImage = backgroundImage;

        // Set the Enemy Character
        // NOTE: If you have multiple enemy characters, choose a random one
        int index = Random.Range(0, encounterableEnemies.Count);
        BattleSceneHandler.enemyCharacter = encounterableEnemies[index];

        // Set Music to play
        BattleSceneHandler.musicToPlay = music;

        // Load Player's Abilities
        // BattleSceneHandler.playerAbilities

        // Transition to Battle Scene
        SceneManager.LoadScene(battleScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player Enters Tall Grass
        if (collision.gameObject.tag == "Player")
        {
            playerCanEncounter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Player Exits Tall Grass
        if (collision.gameObject.tag == "Player")
        {
            playerCanEncounter = false;
        }
    }
}
