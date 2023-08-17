using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class JenAstaDialogueScript : MonoBehaviour, InteractScriptInterface
{
    public bool alreadyInteracted1 = false;
    public bool alreadyInteracted2 = false;

    [SerializeField]
    private CinemachineVirtualCamera dialogueCam;

    [SerializeField]
    private ObjectDialogue dialogue1;

    [SerializeField]
    private ObjectDialogue dialogue2;

    [SerializeField]
    private DialogueManagerScript dialogueManager;

    [SerializeField]
    private MaxInteractScript maxInteractScript;
    public void interact()
    {
        if (!alreadyInteracted1 && !maxInteractScript.alreadyInteracted2)
        {
            dialogueManager.StartDialogue(dialogue1, dialogueCam);
            alreadyInteracted1 = true;
        }
        else if (maxInteractScript.alreadyInteracted2 && !alreadyInteracted2)
        {
            dialogueManager.StartDialogue(dialogue2, dialogueCam);
            alreadyInteracted1 = true;
            alreadyInteracted2 = true;
        }
    }
    public bool canInteract()
    {
        if (!alreadyInteracted1)
        {
            return !alreadyInteracted1;
        }
        if (alreadyInteracted1 && !alreadyInteracted2)
        {
            return !alreadyInteracted2 && maxInteractScript.alreadyInteracted2;
        }
        return false;
    }
}
