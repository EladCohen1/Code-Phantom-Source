using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZlorpFoundDialogueScript : MonoBehaviour
{
    [SerializeField]
    ObjectDialogue dialogue;
    [SerializeField]
    CinemachineVirtualCamera dialogueCam;
    ZlorpNavMeshScript zlorp;
    [SerializeField]
    DialogueManagerScript dialogueManager;
    private bool alreadyInteracted = false;

    void Start()
    {
        zlorp = GameObject.Find("Zlorp").GetComponent<ZlorpNavMeshScript>();
        dialogue.sentences[3].actionAfterSentence = delegate ()
        {
            zlorp.isPatroling = true;
        };
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
