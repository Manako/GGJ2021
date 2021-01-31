using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] string yarnStartNode = "Start";
    [SerializeField] YarnProgram yarnDialog;
    public string targetNode = "";
    [Header("Optional")]
    public YarnProgram scriptToLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (scriptToLoad != null)
        {
            DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.Add(scriptToLoad);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
