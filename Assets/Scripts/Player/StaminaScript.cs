using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    public float stamina;
    [SerializeField] float maxStamina;
    public Slider staminaBar;
    public float dValue;

    public bool isStaminaRecovering = false;
    // Start is called before the first frame update
    void Start()
    {
        staminaBar.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.value = stamina;
    }

    public void DecreaseStamina()
    {
        if (stamina > 0)
        {
            stamina -= dValue * Time.deltaTime;
        }
        else
        {
            isStaminaRecovering = true;
        }
    }

    public void IncreaseStamina()
    {
        if (stamina < maxStamina)
        {
            stamina += dValue * Time.deltaTime;
        }
        else
        {
            isStaminaRecovering = false;
        }
    }
}
