using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InteractLogicScript : MonoBehaviour
{
    [SerializeField]
    private float hitRange = 0.5f;
    private RaycastHit hit;
    [SerializeField]
    private LayerMask interactableNPCLayerMask;

    [SerializeField]
    private Canvas interactGuideUI;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * hitRange, UnityEngine.Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, hitRange, interactableNPCLayerMask))
        {
            if (hit.collider.GetComponent<InteractScriptInterface>() != null)
            {
                if (hit.collider.GetComponent<InteractScriptInterface>().canInteract())
                {
                    interactGuideUI.enabled = true;
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hit.collider.GetComponent<InteractScriptInterface>().interact();
                        interactGuideUI.enabled = false;
                    }
                }
            }
        }
        else
        {
            interactGuideUI.enabled = false;
        }
    }
}
