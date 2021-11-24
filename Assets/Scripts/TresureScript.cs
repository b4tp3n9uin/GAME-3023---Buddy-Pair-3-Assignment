using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureScript : MonoBehaviour
{
    bool Open;
    public int upgradeValue;
    public Animator TresAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Open = false;
        TresAnimator.SetBool("IsOpen", Open);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // When you interact with the Tresure Chest, you get an upgrade on Your Powerups and uses.
            Open = true;
            TresAnimator.SetBool("IsOpen", Open);
        }
    }
}
