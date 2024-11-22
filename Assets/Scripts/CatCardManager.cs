using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatCardManager : MonoBehaviour
{
    public GameObject[] games;

    [Header("Throne Rivalry")]
    public Vector3[] slots;
    public CardController[] random10;
    [Header("Bicolor Racing")]
    public CardController[] bicolorDeck;

    public GameObject deck;
    public GameObject shuffle;

    public GameObject[] ksGame;
    int ks;
    // Start is called before the first frame update
    void Start()
    {
        slots = new Vector3[random10.Length];
        for (int i = 0; i < random10.Length; i++)
        {
            slots[i]= random10[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i < games.Length; i++)
            {
                games[i].SetActive(false);
            }
            games[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < games.Length; i++)
            {
                games[i].SetActive(false);
            }
            games[1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 0; i < games.Length; i++)
            {
                games[i].SetActive(false);
            }
            games[2].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.D))
            deck.SetActive(!deck.activeSelf);
        if (Input.GetKeyDown(KeyCode.S))
            shuffle.SetActive(!shuffle.activeSelf);

        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < random10.Length; i++)
            {
                random10[i].Flop();
            }
            for (int i = 0; i < bicolorDeck.Length; i++)
            {
                bicolorDeck[i].Flop();
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < random10.Length; i++)
            {
                random10[i].Fold();
                for (int j = 0; j < slots.Length; j++)
                {
                    if (random10[i].transform.position == slots[j])
                        random10[i].back.GetComponentInChildren<Text>().text = (j + 1).ToString();
                }
            }
        }


    }

    public void Ready()
    {
        
    }

    public void SwitchDuel()
    {
        for (int i = 0; i < ksGame.Length; i++)
        {
            ksGame[i].SetActive(false);
        }
        ks++;
        ks = (int)Mathf.Repeat(ks, ksGame.Length);
        ksGame[ks].SetActive(true);
    }
    public void Shuffle()
    {

    }
}
