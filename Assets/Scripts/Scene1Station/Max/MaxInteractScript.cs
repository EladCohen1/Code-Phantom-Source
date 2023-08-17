using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MaxInteractScript : MonoBehaviour, InteractScriptInterface
{
    [SerializeField]
    private CinemachineVirtualCamera dialogueCam;

    [SerializeField]
    private DialogueManagerScript dialogueManager;

    [SerializeField]
    private ObjectDialogue dialogue1;

    [SerializeField]
    private ObjectDialogue dialogue2;

    [SerializeField]
    private MachineInteractScript machineInteractScript;

    public bool alreadyInteracted1 = false;
    public bool alreadyInteracted2 = false;

    [SerializeField]
    private DoorCont doorScript;

    void Start()
    {
        dialogue1.sentences[4].actionAfterSentence = delegate ()
        {
            doorScript.LockState(false);
        };
    }

    public void interact()
    {
        if (!alreadyInteracted1)
        {
            dialogueManager.StartDialogue(dialogue1, dialogueCam);
            alreadyInteracted1 = true;
        }
        else if (alreadyInteracted1 && !alreadyInteracted2)
        {
            dialogueManager.StartDialogue(dialogue2, dialogueCam);
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
            return !alreadyInteracted2 && machineInteractScript.alreadyInteracted;
        }
        return false;
    }
}
