using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI sign_Msg;
    public GameObject SignPannel;
    public string message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Display Info Message about the World.
            Time.timeScale = 0.0f;
            Debug.Log("Entered");
            sign_Msg.text = message;
            SignPannel.SetActive(true);
        }
    }
}
