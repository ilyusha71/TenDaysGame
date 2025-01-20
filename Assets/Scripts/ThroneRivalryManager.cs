using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroneRivalryManager : RearrangementController
{
    private CardController[] cards;
    private readonly int[] order = { 8, 7, 9, 6, 10, 5, 1, 4, 2, 3 };
    protected override void Initialize()
    {
        base.Initialize();
        cards = GetComponentsInChildren<CardController>();
    }
    // Update is called once per frame
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
        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].Fold();
                for (int j = 0; j < slots.Length; j++)
                {
                    if (cards[i].transform.position == pos[j])
                        cards[i].back.GetComponentInChildren<Text>().text = (order[j]).ToString();
                }
            }
        }
    }
}
