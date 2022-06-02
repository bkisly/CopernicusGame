using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Subtitle
{
    public float StartTime;
    public float EndTime;

    [TextArea(3, 5)] public string Text;
}
