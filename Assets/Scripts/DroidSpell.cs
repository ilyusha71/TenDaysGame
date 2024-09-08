using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroidSpell : MonoBehaviour
{
    public Image[] spell;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            spell[0].enabled = !spell[0].enabled;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            spell[1].enabled = !spell[1].enabled;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            spell[2].enabled = !spell[2].enabled;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            spell[3].enabled = !spell[3].enabled;
    }
}
