using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LiarBarManager : MonoBehaviour
{
    public Transform[] decks;
    bool[] check;
    public Transform[] expand;
    public Transform[] cards;
    public Transform[] sequence;
    public List<int> lotteryPool = new List<int>();
    public Image imgSponsor;
    public Text nameSponsor;
    public Sprite[] spSponsors;
    public LoadPlayerAvatar lpa;
    public Sprite rip;
    public GameObject greatDemon;
    public RandomDice[] dice;
       // Start is called before the first frame update
    void Start()
    {
        check = new bool[decks.Length];
        sequence = new Transform[cards.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            lotteryPool.Add(i);
        }
        RandomlyAllocate();
        ThrowDice();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RandomlyAllocate();
        if (Input.GetKeyDown(KeyCode.D))
            ThrowDice();
        if (Input.GetKeyDown(KeyCode.F1))
            Expande(0);
        if (Input.GetKeyDown(KeyCode.F2))
            Expande(1);
        if (Input.GetKeyDown(KeyCode.F3))
            Expande(2);
        if (Input.GetKeyDown(KeyCode.F4))
            Expande(3);
        if (Input.GetKeyDown(KeyCode.G))
            greatDemon.SetActive(!greatDemon.activeSelf);

        if (Input.GetKey(KeyCode.LeftShift) && EventSystem.current.currentSelectedGameObject)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<CardController>())
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(decks[0], true);
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(decks[1], true);
                if (Input.GetKeyDown(KeyCode.Alpha3))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(decks[2], true);
                if (Input.GetKeyDown(KeyCode.Alpha4))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(decks[3], true);
            }
        }
    }
    public void Shoot(int index)
    {
        lpa.ChangeAvatar(index, rip);
    }
    public void ThrowDice()
    {
        for (int i = 0; i < dice.Length; i++) { dice[i].Throw(); }
    }
    public void RandomlyAllocate()
    {
        List<int> rand = lotteryPool.ToList();
        for (int i = 0; i < cards.Length; i++)
        {
            int index = Random.Range(0, rand.Count);
            //print(rand[index]);
            if (rand[index] == 20)
            {
                cards[i].GetComponent<CardController>().PlayCard();
                cards[i].GetComponent<Image>().enabled=true;
                cards[i].localPosition = Vector3.zero;
            }             
            else
            {
                cards[i].SetParent(decks[rand[index] % 4]);
                cards[i].localPosition = Vector3.zero;
                cards[i].GetComponent<CardController>().Fold();
                cards[i].GetComponent<Image>().enabled = false;
                sequence[rand[index]] = cards[i];
            }
            rand.Remove(rand[index]);
        }
        int order = Random.Range(0, spSponsors.Length);
       imgSponsor.sprite=spSponsors[order];
        switch (order)
        {
            case 0:
                nameSponsor.text = "烤猪";
                break;
            case 1:
                nameSponsor.text = "柠檬";
                break;
            case 2:
                nameSponsor.text = "贪吃芙";
                break;
        }
        for (int i = 0; i < check.Length; i++)
        {
            check[i] = false;
        }
    }
    public void Expande(int index)
    {
        int order = 0;
        CardController[] cards = decks[index].GetComponentsInChildren<CardController>();

        for (int i = 0; i < cards.Length; i++)
        {
            if (!check[index])
            {
                if (order < 5)
                    cards[i].transform.position = expand[order].position;
                cards[i].Flop();
            }
            else
            {
                cards[i].transform.localPosition = Vector3.zero;
                cards[i].Fold();
            }               
            order++;
        }
        check[index] = !check[index];
    }   
}
