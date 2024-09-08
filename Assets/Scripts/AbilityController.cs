using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{
    public Transform ability;
    public Image imgAbility;
    int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (imgAbility.enabled) 
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                index--;
                ability.rotation = Quaternion.Euler(0, 0, 36 * index);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                index++;
                ability.rotation = Quaternion.Euler(0, 0, 36 * index);
            }
        }
    }
    public void Trigger()
    {
        imgAbility.enabled = !imgAbility.enabled;
    }
}
