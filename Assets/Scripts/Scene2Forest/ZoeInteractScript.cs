using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ZoeInteractScript : MonoBehaviour, InteractScriptInterface
{
    public bool alreadyInteracted1 = false;
    public bool alreadyInteracted2 = false;
    public bool alreadyInteracted3 = false;

    [SerializeField]
    private CinemachineVirtualCamera dialogueCam1;
    [SerializeField]
    private CinemachineVirtualCamera dialogueCam2;
    [SerializeField]
    private CinemachineVirtualCamera dialogueCam3;

    [SerializeField]
    private ObjectDialogue dialogue1;
    [SerializeField]
    private ObjectDialogue dialogue2;
    [SerializeField]
    private ObjectDialogue dialogue3;

    [SerializeField]
    private DialogueManagerScript dialogueManager;

    [SerializeField]
    private Canvas jumpScareCanvas;

    [SerializeField]
    private AudioSource jumpScareAudio;

    [SerializeField]
    private AudioSource backgroundMusic;

    [SerializeField]
    private AudioSource zoeLaughingSoundEffect;

    private bool isTalking = false;
    void Start()
    {
        dialogue1.sentences[2].actionAfterSentence = delegate ()
        {
            transform.position = new Vector3(323.905f, 0.041f, 309.938f);
            isTalking = false;
        };

        dialogue2.sentences[2].actionAfterSentence = delegate ()
        {
            isTalking = false;
            zoeLaughingSoundEffect.enabled = false;
            transform.position = new Vector3(321.09f, 0.041f, 338.72f);
        };

        dialogue3.sentences[2].actionAfterSentence = delegate ()
        {
            isTalking = false;
            backgroundMusic.enabled = false;
            jumpScareAudio.enabled = true;
            StartCoroutine(showJumpScareCanvas());
            StartCoroutine(showJumpScareCanvas());
        };

    }
    public void interact()
    {
        if (!alreadyInteracted1)
        {
            isTalking = true;
            dialogueManager.StartDialogue(dialogue1, dialogueCam1);
            alreadyInteracted1 = true;
        }
        else if (!alreadyInteracted2)
        {
            isTalking = true;
            dialogueManager.StartDialogue(dialogue2, dialogueCam2);
            alreadyInteracted2 = true;
        }
        else if (!alreadyInteracted3)
        {
            isTalking = true;
            dialogueManager.StartDialogue(dialogue3, dialogueCam3);
            alreadyInteracted3 = true;
        }

    }
    public bool canInteract()
    {
        if (!isTalking)
        {
            return !alreadyInteracted1 || !alreadyInteracted2 || !alreadyInteracted3;
        }
        return false;
    }

    IEnumerator showJumpScareCanvas()
    {
        yield return new WaitForSeconds(3.5f);
        jumpScareCanvas.enabled = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);
    }
}
