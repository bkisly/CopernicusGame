using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerConfigInfo
{
    public int LevelIndex;

    public float PositionX;
    public float PositionY;
    public float CurrentHealth;

    public int[] Items;
    public int[] EquippedItems;
    public int Points;

    public PlayerConfigInfo(int levelIndex, Vector2 playerPosition, float currentHealth, IEnumerable<int> items, IEnumerable<int> equippedItems, int points)
    {
        LevelIndex = levelIndex;

        PositionX = playerPosition.x;
        PositionY = playerPosition.y;
        CurrentHealth = currentHealth;

        Items = items.ToArray();
        EquippedItems = equippedItems.ToArray();
        Points = points;
    }
}
