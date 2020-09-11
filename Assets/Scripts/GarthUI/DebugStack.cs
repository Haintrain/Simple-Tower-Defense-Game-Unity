using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStack : MonoBehaviour
{
    //Debug.Log will show the stacktrace next to the message, allowing
    //us to find which script is disabling the wrong GameObject.
    void OnDisable()
    {
        Debug.Log("disabled");
    }
}
