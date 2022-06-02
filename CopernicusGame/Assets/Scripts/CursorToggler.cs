using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToggler : MonoBehaviour
{
    public void ToggleCursor(bool enable)
    {
        Cursor.visible = enable;
    }
}
