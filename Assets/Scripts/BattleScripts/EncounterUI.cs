using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Will prompt the user what to do, and enable/disable UI elements at different times
public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    private EncounterInstance encounter;

    [SerializeField]
    private TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    private float timeBetweenCharacters = 0.1f;

    [SerializeField]
    private GameObject abilityPanel;

    // This is a reference to keep track of our coroutine
    private IEnumerator animateTextCoroutineRef = null;
    private IEnumerator animatePlayerHPCoroutineRef = null;
    private IEnumerator animateEnemyHPCoroutineRef = null;


    // Start is called before the first frame update
    void Awake()
    {
        animateTextCoroutineRef = AnimateTextCoroutine("You have encountered an opponent!", timeBetweenCharacters);

        // Animate text - say what was encountered
        StartCoroutine(animateTextCoroutineRef);

        // OnCharacterTurnBegin, announce whose turn it is now
        encounter.onCharacterTurnBegin.AddListener(AnnounceCharacterTurnBegin);

        // On Player turn begin, enable UI
        encounter.onPlayerTurnBegin.AddListener(EnablePlayerUI);

        // On Player turn end, disable UI
        encounter.onPlayerTurnEnd.AddListener(DisablePlayerUI);

        // On Character use ability, print out ability information
        encounter.onCharacterAbilityUsed.AddListener(AnnounceCharacterAbilityUsed);

        // On Hp Change, change UI
        encounter.onHPChange.AddListener(AnimateHealthBars);

        // On Player tried to use an ability out of uses
        encounter.onTriedAbilityOutOfUses.AddListener(AnnounceAbilityOutOfUses);

        // On Battle Won, display the defeated enemy
        encounter.onWinBattle.AddListener(AnnounceBattleOver);
    }

    void AnnounceCharacterTurnBegin(ICharacter characterTurn)
    {
        AnimateText("It is the " + characterTurn.name + "'s turn.");
    }

    void AnnounceCharacterAbilityUsed(ICharacter characterTurn, Ability abilityUsed)
    {
        AnimateText(characterTurn.name + " used " + abilityUsed.name + "!");
    }

    void AnnounceAbilityOutOfUses(Ability ability)
    {
        AnimateText(ability.name + " is out of uses!");
    }

    void AnnounceBattleOver(ICharacter enemy)
    {
        // Stop printing out the current message
        if (animateTextCoroutineRef != null)
        {
            StopCoroutine(animateTextCoroutineRef);
        }

        // Start printing out the new message
        animateTextCoroutineRef = BattleWonCoroutine(enemy.name + " was defeated!", timeBetweenCharacters);
        StartCoroutine(animateTextCoroutineRef);
    }

    void AnimateText(string msg)
    {
        // Stop printing out the current message
        if (animateTextCoroutineRef != null)
        {
            StopCoroutine(animateTextCoroutineRef);
        }

        // Start printing out the new message
        animateTextCoroutineRef = AnimateTextCoroutine(msg, timeBetweenCharacters);
        StartCoroutine(animateTextCoroutineRef);
    }

    void EnablePlayerUI(ICharacter characterTurn)
    {
        abilityPanel.SetActive(true);
    }

    void DisablePlayerUI(ICharacter characterTurn)
    {
        abilityPanel.SetActive(false);
    }

    public IEnumerator AnimateTextCoroutine(string message, float secondsPerCharacter = 0.1f)
    {
        // Reset message and print character by character
        encounterText.text = "";
        for (int currentCharacter = 0; currentCharacter < message.Length; currentCharacter++)
        {
            encounterText.text += message[currentCharacter];
            yield return new WaitForSeconds(secondsPerCharacter);
        }

        animateTextCoroutineRef = null;
    }

    public IEnumerator BattleWonCoroutine(string message, float secondsPerCharacter = 0.1f)
    {
        // Reset message and print character by character
        encounterText.text = "";
        for (int currentCharacter = 0; currentCharacter < message.Length; currentCharacter++)
        {
            encounterText.text += message[currentCharacter];
            yield return new WaitForSeconds(secondsPerCharacter);
        }

        animateTextCoroutineRef = null;

        // Send player after battle over message is done
        yield return new WaitForSeconds(2.0f);

        if (PlayerBehaviour.bosses == 0)
            SceneManager.LoadScene("WinScene");
        else
        {
            PlayerBehaviour.keys += Random.RandomRange(1, 3);
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void AnimateHealthBars(ICharacter player, ICharacter enemy)
    {
        if (animatePlayerHPCoroutineRef != null)
        {
            StopCoroutine(animatePlayerHPCoroutineRef);
        }

        if (animateEnemyHPCoroutineRef != null)
        {
            StopCoroutine(animateEnemyHPCoroutineRef);
        }

        animatePlayerHPCoroutineRef = AnimatePlayerHealthBarCoroutine(player.HPBar, player);
        StartCoroutine(animatePlayerHPCoroutineRef);

        animateEnemyHPCoroutineRef = AnimateEnemyHealthBarCoroutine(enemy.HPBar, enemy);
        StartCoroutine(animateEnemyHPCoroutineRef);
    }

    public IEnumerator AnimatePlayerHealthBarCoroutine(Slider hpBar, ICharacter character)
    {
        // Save bar's current position
        float currentHP = hpBar.value;

        float endHP = character.HealthPercent;

        // While not close enough to end of animation, keep animating health bar
        while (Mathf.Abs(currentHP - endHP) > 0.01f)
        {
            hpBar.value = Mathf.Lerp(hpBar.value, endHP, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        animatePlayerHPCoroutineRef = null;
    }


    public IEnumerator AnimateEnemyHealthBarCoroutine(Slider hpBar, ICharacter character)
    {
        // Save bar's current position
        float currentHP = hpBar.value;

        float endHP = character.HealthPercent;

        // While not close enough to end of animation, keep animating health bar
        while (Mathf.Abs(currentHP - endHP) > 0.01f)
        {
            hpBar.value = Mathf.Lerp(hpBar.value, endHP, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        animateEnemyHPCoroutineRef = null;
    }

}
