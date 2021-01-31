using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class playerScript : MonoBehaviour

{
    public YarnProgram scriptToLoad;
    [SerializeField] YarnProgram yarnDialog;
    public CharacterController pControls;
    public float speed = 12f;

    Vector3 velocity;
    bool isGrounded;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float interactionRadius = 2.0f;


    private void Start()
    {

    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        pControls.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        pControls.Move(velocity * Time.deltaTime);

        // Detect if we want to start a conversation
        if (Input.GetButtonDown("Fire2"))
        {
            CheckForNearbyNPC();
        }

        // check if the input is "e" and isTriggered=true
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckForNearbyNPC();
        }
    }
    public void CheckForNearbyNPC()
    {
        var allParticipants = new List<InteractableObject>(FindObjectsOfType<InteractableObject>());
        var target = allParticipants.Find(delegate (InteractableObject p) {
            return string.IsNullOrEmpty(p.targetNode) == false && // has a conversation node?
            (p.transform.position - this.transform.position)// is in range?
            .magnitude <= interactionRadius;
        });
        if (target != null)
        {
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(target.targetNode);
        }
    }
}
