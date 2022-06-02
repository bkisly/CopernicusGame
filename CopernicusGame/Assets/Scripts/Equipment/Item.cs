using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int ID = 0;
    public string Name = "New Item";
    public string Description = "Description";
    public Sprite Icon = null;
}
