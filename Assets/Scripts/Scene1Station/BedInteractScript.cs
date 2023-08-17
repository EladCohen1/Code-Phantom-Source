using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class BedInteractScript : MonoBehaviour, InteractScriptInterface
{
    public bool alreadyInteracted = false;

    [SerializeField]
    private CinemachineVirtualCamera dialogueCam;

    [SerializeField]
    private ObjectDialogue dialogue;

    [SerializeField]
    private DialogueManagerScript dialogueManager;

    [SerializeField]
    private JenAstaDialogueScript jenAstaDialogueScript;
    void Start()
    {
        dialogue.sentences[0].actionAfterSentence = delegate ()
        {
            SceneManager.LoadScene(3);
        };
    }
    public void interact()
    {
        dialogueManager.StartDialogue(dialogue, dialogueCam);
        alreadyInteracted = true;
    }
    public bool canInteract()
    {
        return !alreadyInteracted && jenAstaDialogueScript.alreadyInteracted2;
    }
}
