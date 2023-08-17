using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CaptainRoomEnteredDialogueScript : MonoBehaviour
{
    [SerializeField]
    ObjectDialogue dialogue;
    [SerializeField]
    CinemachineVirtualCamera dialogueCam;
    [SerializeField]
    DialogueManagerScript dialogueManager;
    public bool alreadyInteracted = false;

    void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !alreadyInteracted)
        {
            alreadyInteracted = true;
            dialogueManager.StartDialogue(dialogue, dialogueCam);
        }
    }
}
