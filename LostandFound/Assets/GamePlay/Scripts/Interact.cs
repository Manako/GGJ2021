using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Interact : MonoBehaviour
{
    public bool isInteracting;
    public bool playDialogue;

    private void OnTriggerEnter(Collider other)
    {
        // check if other is Player
        CharacterController player = other.GetComponent<CharacterController>();
        if (player != null)
        {
            if (isInteracting)
            {
                playDialogue = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // check if other is Player
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if the input is "e" and isTriggered=true
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInteracting)
            {
                isInteracting = false;
                playDialogue = false;
            }
            else
            {
                isInteracting = true;
            }
        }
    }
}
