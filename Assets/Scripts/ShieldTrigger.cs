using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldTrigger : MonoBehaviour
{
    public Image shield;
    public void Trigger()
    {
        shield.enabled = !shield.enabled;
    }
}
