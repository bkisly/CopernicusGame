using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaves as a model class for the equipment system. Stores all data about items and shares methods that can modify its contents.
/// </summary>
public class Equipment : MonoBehaviour
{
    public static Equipment instance;
    public List<Item> Items;
    public List<Item> EquippedItems;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Adds the given item to the equipment.
    /// </summary>
    public void Add(Item item)
    {
        Items.Add(item);
    }

    /// <summary>
    /// Removes the given item from the equipment.
    /// </summary>
    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    /// <summary>
    /// Moves the given item from the equipment (unused runes) to the EquippedRunes.
    /// </summary>
    public void MoveToEquippedRunes(Item item)
    { 
        if (EquippedItems.Count < 5)
        {
            EquippedItems.Add(item);
            Remove(item);
        }
    }

    /// <summary>
    /// Moves the given item from the EquippedRunes to the equipment (unused runes).
    /// </summary>
    public void MoveToEquipment(Item item)
    {
        Add(item);
        EquippedItems.Remove(item);
    }
}
