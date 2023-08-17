using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneStartDialogueScript : MonoBehaviour
{
    [SerializeField]
    private ObjectDialogue dialogue;

    [SerializeField]
    private DialogueManagerScript dialogueManager;

    [SerializeField]
    private CinemachineVirtualCamera dialogueCam;

    [SerializeField]
    private CinemachineFreeLook mainCam;
    // Start is called before the first frame update

    void Awake()
    {
        mainCam.enabled = false;
    }
    void Start()
    {
        dialogueManager.StartDialogue(dialogue, dialogueCam);
    }


}
