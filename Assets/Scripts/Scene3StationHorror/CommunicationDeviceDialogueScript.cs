using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CommunicationDeviceDialogueScript : MonoBehaviour, InteractScriptInterface
{
    public bool alreadyInteracted = false;

    [SerializeField]
    ObjectDialogue dialogue;
    [SerializeField]
    CinemachineVirtualCamera dialogueCam;
    [SerializeField]
    DialogueManagerScript dialogueManager;
    [SerializeField]
    Canvas blackScreen;
    [SerializeField]
    AudioSource backgroundMusic;
    [SerializeField]
    AudioSource endBackgroundMusic;
    [SerializeField]
    AudioSource zlorpBangsOnDoor;

    [SerializeField] CaptainRoomDoorLockInteractScript captainRoomDoorLockInteractScript;


    void Start()
    {
        dialogue.sentences[20].actionAfterSentence = delegate ()
        {
            backgroundMusic.enabled = false;
            endBackgroundMusic.enabled = true;
        };
        dialogue.sentences[31].actionAfterSentence = delegate ()
        {
            blackScreen.enabled = true;
        };
        dialogue.sentences[32].actionAfterSentence = delegate ()
        {
            zlorpBangsOnDoor.enabled = true;
        };
        dialogue.sentences[37].actionAfterSentence = delegate ()
        {
            SceneManager.LoadScene(1);
        };
    }

    public void interact()
    {
        alreadyInteracted = true;
        dialogueManager.StartDialogue(dialogue, dialogueCam);
    }

    public bool canInteract()
    {
        return captainRoomDoorLockInteractScript.alreadyInteracted && !alreadyInteracted;
    }
}
