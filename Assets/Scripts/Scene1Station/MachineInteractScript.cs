using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MachineInteractScript : MonoBehaviour, InteractScriptInterface
{
    public Material green;
    public Material redLight;
    public Renderer gameobjRenderer;

    public bool alreadyInteracted = false;

    [SerializeField]
    private CinemachineVirtualCamera dialogueCam;

    [SerializeField]
    private ObjectDialogue dialogue;

    [SerializeField]
    private DialogueManagerScript dialogueManager;

    [SerializeField]
    private MaxInteractScript maxInteractScript;

    void Start()
    {
        dialogue.sentences[1].actionAfterSentence = delegate ()
        {
            gameobjRenderer.material = green;
        };
    }
    public void interact()
    {
        dialogueManager.StartDialogue(dialogue, dialogueCam);
        alreadyInteracted = true;
    }
    public bool canInteract()
    {
        return !alreadyInteracted && maxInteractScript.alreadyInteracted1;
    }
}
