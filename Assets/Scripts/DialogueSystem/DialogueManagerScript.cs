using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class DialogueManagerScript : MonoBehaviour
{
    [SerializeField]
    private Canvas dialogueUI;
    public PlayerScript playerScript;
    private Queue<SentenceObject> sentences;

    [SerializeField]
    private Text dialogueName;

    [SerializeField]
    private Text dialogueText;

    private bool isTyping;

    private SentenceObject lastDequeued;

    CinemachineVirtualCamera dialogueCam;

    void Awake()
    {
        dialogueUI.enabled = false;
        sentences = new Queue<SentenceObject>();
    }
    public void StartDialogue(ObjectDialogue dialogue, CinemachineVirtualCamera dialogueCamIn)
    {
        dialogueCam = dialogueCamIn;
        dialogueUI.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        playerScript.isControlled = false;
        dialogueCam.enabled = true;
        playerScript.DisableMainCam();
        lastDequeued = null;
        foreach (SentenceObject sentance in dialogue.sentences)
        {
            sentences.Enqueue(sentance);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        dialogueText.text = "";
        if (isTyping)
        {
            dialogueText.text = lastDequeued.text;
            StopAllCoroutines();
            isTyping = false;
        }
        else
        {
            if (lastDequeued != null && lastDequeued.actionAfterSentence != null)
            {
                lastDequeued.actionAfterSentence();
            }
            if (sentences.Count > 0)
            {
                lastDequeued = sentences.Dequeue();
                dialogueName.text = lastDequeued.name;
                StartCoroutine(TypeSentence(lastDequeued.text));
            }
            else
            {
                EndDialogue();
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        dialogueUI.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerScript.isControlled = true;
        playerScript.EnableMainCam();
        dialogueCam.enabled = false;
    }
}
