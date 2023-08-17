using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CaptainRoomDoorLockInteractScript : MonoBehaviour, InteractScriptInterface
{
    public bool alreadyInteracted = false;
    [SerializeField]
    private DoorCont doorScript;

    [SerializeField]
    ObjectDialogue dialogue;
    [SerializeField]
    CinemachineVirtualCamera dialogueCam;
    [SerializeField]
    DialogueManagerScript dialogueManager;
    [SerializeField] CaptainRoomEnteredDialogueScript captainRoomEnteredDialogueScript;

    public void interact()
    {
        alreadyInteracted = true;
        doorScript.LockState(true);
        dialogueManager.StartDialogue(dialogue, dialogueCam);
    }

    public bool canInteract()
    {
        return captainRoomEnteredDialogueScript.alreadyInteracted && !alreadyInteracted;
    }
}
