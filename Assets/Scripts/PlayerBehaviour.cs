using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerDirection
{
    FORWARD = 0,
    RIGHT = 1,
    BACKWARD = 2,
    LEFT = 3
}

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 5;
    public Animator animatior;
    public PlayerDirection direction = PlayerDirection.FORWARD;

    private string playerLocationSaveKey;

    // int values to count how much inventory you have.
    public static int healUse = 5, shieldUse = 5, powerUse = 5, keys = 2, bosses = 3;

    // float values for the attack/heal values for the abilities.
    public static float smlAtk_value = 10, lrgAtk_value = 20, heal_value = 15;

    // bool value for the player shield.
    public static bool isProtected = false;

    public TMPro.TextMeshProUGUI keyText, bossText;

    public bool OnGrass, isWalking;

    [Header("Bosses")]
    public GameObject GiantOwl;
    public GameObject RagingBull;
    public GameObject Vroot;

    // Start is called before the first frame update
    void Start()
    {
        // Save the name of the save keys
        playerLocationSaveKey = gameObject.name + "Location";

        // Load Player's Loction
        LoadPlayerLocation();

        // Display the number of remaining bosses left.
        CheckBossCount();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayKeyText();

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0));

        IdleWalk();
    }

    public void IdleWalk()
    {
        // When player is moving in certain directions, animation changes.
        if (Input.GetAxis("Horizontal") > 0) // move right
        {
            isWalking = true;
            direction = PlayerDirection.RIGHT;
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", isWalking);
        }
        else if (Input.GetAxis("Horizontal") < 0) // move left
        {
            isWalking = true;
            direction = PlayerDirection.LEFT;
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", isWalking);
        }
        else if (Input.GetAxis("Vertical") > 0) // move up
        {
            isWalking = true;
            direction = PlayerDirection.BACKWARD;
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", isWalking);
        }
        else if (Input.GetAxis("Vertical") < 0) // move down
        {
            isWalking = true;
            direction = PlayerDirection.FORWARD;
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", isWalking);
        }
        else // idle
        {
            isWalking = false;
            FindObjectOfType<AudioManager>().Stop("WalkNormal");
            FindObjectOfType<AudioManager>().Stop("WalkGrass");
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", isWalking);
        }

        //Loop walk sound
        if (isWalking && OnGrass)
        {
            FindObjectOfType<AudioManager>().LoopSound("WalkGrass");
        }
        else if (isWalking && !OnGrass)
        {
            FindObjectOfType<AudioManager>().LoopSound("WalkNormal");
        }
    }

    public void SavePlayerLocation(float XDisAway, float YDisAway)
    {
        // Save the Player's location (x, y, z)
        string playerLocation = "";

        playerLocation += (transform.position.x + XDisAway) + ",";
        playerLocation += (transform.position.y + YDisAway) + ",";
        playerLocation += transform.position.z + ",";

        PlayerPrefs.SetString(playerLocationSaveKey, playerLocation);
    }
    
    private void LoadPlayerLocation()
    {
        // Check if the key exists...
        if (!PlayerPrefs.HasKey(playerLocationSaveKey))
        {
            return;
        }

        // Get saved location
        string savedLocation = PlayerPrefs.GetString(playerLocationSaveKey, "");

        string[] locationData = savedLocation.Split(',');

        // Set Player's Location
        transform.localPosition = new Vector3(float.Parse(locationData[0]), float.Parse(locationData[1]), float.Parse(locationData[2]));
    }

    void DisplayKeyText()
    {
        keyText.text = "Keys: " + keys;
        bossText.text = "Boss Left: " + bosses;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().Play("Hit");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EncounterArea"))
        {
            // Walking in Encounter area will make you slower
            speed = 3;
            OnGrass = true;
        }

        float XdistanceAway = -3.5f;
        float YdistanceAway = -5.0f; // float value for the Distance away from boss, so you don't constantly collide and go to battle scene.

        // Go into Battle with Boss.
        if (other.gameObject.CompareTag("GiantOwl"))
        {
            SceneManager.LoadScene("BossSceneGiantOwl");
            SavePlayerLocation(0.0f, YdistanceAway);
        }
        if (other.gameObject.CompareTag("RagingBull"))
        {
            SceneManager.LoadScene("BossSceneRagingBull");
            SavePlayerLocation(0.0f,YdistanceAway);
        }
        if (other.gameObject.CompareTag("Vroot"))
        {
            SceneManager.LoadScene("BossSceneVroot");
            SavePlayerLocation(XdistanceAway, 0.0f);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EncounterArea"))
        {
            speed = 5;
            OnGrass = false;
        }
    }

    void CheckBossCount()
    {
        if (bosses <= 0)
            bosses = 3;

        if (bosses == 2)
        {
            GiantOwl.SetActive(false);
            RagingBull.SetActive(true);
            Vroot.SetActive(true);
        }
        else if (bosses == 1)
        {
            GiantOwl.SetActive(false);
            RagingBull.SetActive(false);
            Vroot.SetActive(true);
        }
        else
        {
            GiantOwl.SetActive(true);
            RagingBull.SetActive(true);
            Vroot.SetActive(true);
        }
    }

    public void OnResetPositionToStartButtonPressed() // Resets the Player to his  origin Location.
    {
        Vector3 OriginPosition = new Vector3(-1.0f, 0.0f, 0.0f);
        transform.localPosition = OriginPosition;
    }

}
