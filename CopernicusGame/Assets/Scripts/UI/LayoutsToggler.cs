using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayoutsToggler : MonoBehaviour
{
    public void EnableLayouts(params Type[] exceptions)
    {
        if(!exceptions.Contains(typeof(PauseUI))) FindObjectOfType<PauseUI>().enabled = true;
        if(!exceptions.Contains(typeof(EquipmentUI))) FindObjectOfType<EquipmentUI>().enabled = true;
        if(!exceptions.Contains(typeof(PlayerMovement))) FindObjectOfType<PlayerMovement>().enabled = true;
        if(!exceptions.Contains(typeof(PlayerCombat))) FindObjectOfType<PlayerCombat>().enabled = true;
    }

    public void DisableLayouts(params Type[] exceptions)
    {
        if (!exceptions.Contains(typeof(PauseUI))) FindObjectOfType<PauseUI>().enabled = false;
        if (!exceptions.Contains(typeof(EquipmentUI))) FindObjectOfType<EquipmentUI>().enabled = false;
        if (!exceptions.Contains(typeof(PlayerMovement))) FindObjectOfType<PlayerMovement>().enabled = false;
        if (!exceptions.Contains(typeof(PlayerCombat))) FindObjectOfType<PlayerCombat>().enabled = false;
    }
}
