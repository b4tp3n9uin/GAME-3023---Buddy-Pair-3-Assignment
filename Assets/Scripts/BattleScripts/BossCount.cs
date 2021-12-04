using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that will count how many bosses have been defeated.
public class BossCount : MonoBehaviour
{
    AICharacter Boss;
    bool Iterate = false; // bool value to control iteration.

    // Start is called before the first frame update
    void Start()
    {
        Boss = FindObjectOfType<AICharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        BossDefeated();
    }

    void BossDefeated()
    {
        if (Boss.Health <= 0 && !Iterate) // Decrement the boss count to indicated how many more bosses there are.
        {
            PlayerBehaviour.bosses--;
            PlayerBehaviour.keys += 2; // Add more keys when Boss dies.
            Iterate = true;
        }
    }
}
