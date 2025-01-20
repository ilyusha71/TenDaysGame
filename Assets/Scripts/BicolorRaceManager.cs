using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicolorRaceManager : RearrangementController
{
    private CardController[] cards;
    public int offset;
    protected override void Initialize()
    {
        base.Initialize();
        cards = GetComponentsInChildren<CardController>();
        for (int i = 0; i < slots.Length; i++)
        {
            pos[i] +=new Vector3(111-i*37+offset,0,0);
        }
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].Flop();
            }
        }
    }
}
