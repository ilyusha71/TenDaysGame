using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicCatChase : MultiplayerGameManager
{
    public HandManager[] handManager;
    [Header("Mystery Cat")]
    public GameObject panelCat;
    public MysteryCat[] mysteryCat;
    private int currentIndex;

    [Header("Top Hat")]
    public CanvasGroup topHats;
    public RearrangementController hats;
    public override void LoadPlayer()
    {
        base.LoadPlayer();
        PeanutsManager.Instance.dealerName.text = "猫";
    }
    public void Initialize()
    {
        RespawnHat();
        ReturnSlots();
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < hats.slots.Length; i++)
        {
            hats.slots[i].gameObject.SetActive(true);
            int index = i;
            PeanutsManager.Instance.peanutSystems[i].revolver.onClick.AddListener(() => hats.slots[index].gameObject.SetActive(false));
        }
        Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            Initialize();
        if (Input.GetKeyDown(KeyCode.F11))
            panelCat.SetActive(!panelCat.activeSelf);
        if (Input.GetKeyDown(KeyCode.Alpha0))
            ReturnSlots();
        if (Input.GetKeyDown(KeyCode.F1))
            Expande(0);
        if (Input.GetKeyDown(KeyCode.F2))
            Expande(1);
        if (Input.GetKeyDown(KeyCode.F3))
            Expande(2);
        if (Input.GetKeyDown(KeyCode.F4))
            Expande(3);
        if (Input.GetKeyDown(KeyCode.F7))
            SummonCat();
        if (Input.GetKeyDown(KeyCode.H))
            RespawnHat();
        if (Input.GetKeyDown(KeyCode.V))
            Alter();
        if (Input.GetKeyDown(KeyCode.C))
        {
            topHats.alpha = topHats.alpha == 1.0f ? 0.7f : 1.0f;
            hats.ReturnSlots();
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
            Transfer(1);
        if (Input.GetKeyDown(KeyCode.PageUp))
            Transfer(-1);

        if (Input.GetKey(KeyCode.LeftShift) && EventSystem.current.currentSelectedGameObject)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<CardController>())
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(handManager[0].container, true);
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(handManager[1].container, true);
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(handManager[2].container, true);
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(handManager[3].container, true);
            }
        }
    }
    public void Shoot(int index) => hats.slots[index].gameObject.SetActive(false);
    public void RespawnHat()
    {
        for (int i = 0; i < hats.slots.Length; i++)
        {
            hats.slots[i].gameObject.SetActive(true);
        }
    }
    public void ReturnSlots()
    {
        expandPanel.enabled = false;
        for (int i = 0; i < handManager.Length; i++)
        {
            handManager[i].Initialize();
        }
        SummonCat();
    }
    public void SummonCat()
    {    
        currentIndex = Random.Range(0, 4);
        if ( !hats.slots[currentIndex].gameObject.activeSelf) { SummonCat(); return; }
        mysteryCat[4].transform.position = hats.GetPosition(currentIndex);
        mysteryCat[4].RandomElement();
        for (int i = 0; i < mysteryCat.Length-1; i++)
        {
            if (!hats.slots[i].gameObject.activeSelf) continue;
            mysteryCat[i].RandomElement();
        }
    }
    public void Alter()
    {
        mysteryCat[4].RandomElement();
    }
    public void Transfer(int value)
    {
        currentIndex += value;
        currentIndex = (int)Mathf.Repeat(currentIndex, 4);
        if (!hats.slots[currentIndex].gameObject.activeSelf) { Transfer(value); return; }
        mysteryCat[4].transform.position = hats.GetPosition(currentIndex);
    }
    public override void Expande(int index)
    {
        expandPanel.enabled = false;
        for (int hand = 0; hand < handManager.Length; hand++)
        {
            handManager[hand].IsDisplay = hand == index ? !handManager[hand].IsDisplay : false;
            if (handManager[hand].IsDisplay)
            {
                CardController[] cards = handManager[hand].container.GetComponentsInChildren<CardController>();
                for (int i = 0; i < cards.Length; i++)
                {
                    if (i >= expand.Length) return;
                    cards[i].transform.position = expand[i].position;
                    cards[i].Open();
                }
                //CalculateState(hand, cards.Length);
                expandPanel.enabled = true;
            }
            else
            {
                CardController[] cards = handManager[hand].container.GetComponentsInChildren<CardController>();
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].transform.position = handManager[hand].container.position;
                    cards[i].Open();
                }
                //CalculateState(hand, cards.Length);
            }
        }
    }
    void ShowCat()
    {

    }
    //void CalculateState(int index)
    //{
    //    CardController[] cards = container[index].GetComponentsInChildren<CardController>();
    //    peanuts[index].text = cards.Length.ToString();
    //    hats.slots[index].gameObject.SetActive((cards.Length == 0) ? false : true);
    //}
    //void CalculateState(int index,int count)
    //{
    //    peanuts[index].text = count.ToString();
    //    hats.slots[index].gameObject.SetActive((count == 0) ? false : true);
    //}
}
