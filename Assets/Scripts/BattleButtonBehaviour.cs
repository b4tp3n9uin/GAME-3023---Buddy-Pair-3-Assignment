using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleButtonBehaviour : MonoBehaviour
{
    public Animator playerAnimator;
    public string overworldScene = "GameScene";

    public void FleeBattle()
    {
        SceneManager.LoadScene(overworldScene);
    }

    public void PlayAbility(string name)
    {
        playerAnimator.SetTrigger(name);
    }
}
