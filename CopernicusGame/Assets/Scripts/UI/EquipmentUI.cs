using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public GameObject EquipmentPanel;

    public Dropdown EquipmentDropdown;
    public Dropdown EquippedDropdown;

    public Text StrengthText, ArmorText, SpeedText, JumpText;

    private bool _isOpened = false;
    private bool _openable = true;

    private void Update()
    {
        if (_openable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleLayout();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isOpened == true)
                {
                    ToggleLayout();
                }
            }
        }
    }

    public void DisableEquipmentOpening()
    {
        EquipmentPanel.SetActive(false);
        _isOpened = false;
        _openable = false;
    }

    public void MoveToEquipment()
    {
        if (EquippedDropdown.options.Count > 0)
        {
            Equipment.instance.MoveToEquipment(Equipment.instance.EquippedItems[EquippedDropdown.value]);
            UpdateDropdownOptions();
            UpdateStats();
        }
    }

    public void MoveToEquipped()
    {
        if (EquipmentDropdown.options.Count > 0)
        {
            Equipment.instance.MoveToEquippedRunes(Equipment.instance.Items[EquipmentDropdown.value]);
            UpdateDropdownOptions();
            UpdateStats();
        }
    }

    private void ToggleLayout()
    {
        UpdateDropdownOptions();
        UpdateStats();

        _isOpened = !_isOpened;
        EquipmentPanel.SetActive(_isOpened);

        if (_isOpened == true)
        {
            FindObjectOfType<AudioManager>().PlaySound("EquipmentOpen");

            Time.timeScale = 0;
            FindObjectOfType<LayoutsToggler>().DisableLayouts(typeof(EquipmentUI));
            Cursor.visible = true;
        }
        else
        {
            FindObjectOfType<AudioManager>().PlaySound("EquipmentClose");

            Time.timeScale = 1;
            FindObjectOfType<LayoutsToggler>().EnableLayouts(typeof(EquipmentUI));
            Cursor.visible = false;
        }
    }

    private void UpdateDropdownOptions()
    {
        EquipmentDropdown.ClearOptions();
        EquippedDropdown.ClearOptions();

        foreach(Item item in Equipment.instance.Items)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(item.Name, item.Icon);
            EquipmentDropdown.options.Add(optionData);
        }

        foreach(Item item in Equipment.instance.EquippedItems)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(item.Name, item.Icon);
            EquippedDropdown.options.Add(optionData);
        }

        EquipmentDropdown.RefreshShownValue();
        EquippedDropdown.RefreshShownValue();
    }

    private void UpdateStats()
    {
        StrengthText.text = PlayerStats.instance.AttackStrength.ToString();
        ArmorText.text = PlayerStats.instance.Armor.ToString();
        SpeedText.text = PlayerStats.instance.Speed.ToString();
        JumpText.text = PlayerStats.instance.JumpForce.ToString();
    }

    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("UICheck");
    }

    public void PlayHoverSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("UIHover");
    }
}
