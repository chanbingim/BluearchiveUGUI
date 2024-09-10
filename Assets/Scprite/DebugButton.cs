using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    public void DebugOnclicked()
    {
        Debug.Log(this.name + "Clicked");
    }
}
