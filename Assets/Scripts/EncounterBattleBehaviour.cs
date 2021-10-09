using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField]
    private string battleScene = "BattleScene";

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
                // Transition to Battle Scene
                // NOTE: When battle scene is implemented, use this   SceneManager.LoadScene(battleScene);
                Debug.Log("Battle Scene Transition");
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
