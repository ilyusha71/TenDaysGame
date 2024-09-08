using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnRoundCounter : MonoBehaviour, IPointerClickHandler
{
    public bool reverse;
    public int round;
    public Text txtRound;
    public int max;   
    // Start is called before the first frame update
    void Start()
    {
        max = int.Parse(txtRound.text);
        round = max-1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (reverse)
        {
            round--;
            int i = (int)Mathf.Repeat(round, max);
            i++;
            txtRound.text = i.ToString();
        }
        else
        {
            round++;
            int i = (int)Mathf.Repeat(round, max);
            i++;
            txtRound.text = i.ToString();
        }
    }
}
