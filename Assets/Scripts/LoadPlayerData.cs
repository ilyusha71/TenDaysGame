using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class LoadPlayerData : MonoBehaviour
{
    [Header("Player List Panel")]
    public Transform playerListPanel;
    public GameObject playerTab;
    public class Cat
    {
        public Image avatar;
        public Text name;
        //public Sprite avatar;
        //public string name;        
    }
    Dictionary<string,Cat> playerList = new Dictionary<string, Cat>();
    //string[] playerList;
    [Header("Cat")]
    public GameObject cat;
    public Image player;
    public InputField input;
    public GameObject[] pages;
    int index;
    public Text info;
    public Text catbell;
    public Text casino;
    public Text judgement;
    public Text prestige;
    public Text championship;
    public Text pets;
    [Header("Item Panel")]
    public GameObject itemPanel;
    public Text[] items;
    [Header("Skill Panel")]
    public GameObject skillPanel;
    public Text[] skills;
    [Header("NPC Panel")]
    public GameObject npcPanel;
    public Text[] npc;
    [Header("Tasting Panel")]
    public GameObject tastingPanel;
    public Text[] tasting;

    [Header("Royal Trade War")]
    public Text[] rtwRecords;
    [Header("Minefield Assault")]
    public Text[] maRecords;






    private void Awake()
    {
        items = itemPanel.GetComponentsInChildren<Text>();
        skills = skillPanel.GetComponentsInChildren<Text>();
        npc = npcPanel.GetComponentsInChildren<Text>();
        tasting = tastingPanel.GetComponentsInChildren<Text>();
        equipment.Initialize();
        bag.Initialize();
        consumable.Initialize();
        missionItem.Initialize();
        book.Initialize();
        life.Initialize();
        adventure.Initialize();
        combat.Initialize();
        knowledge.Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index++;
            index = (int)Mathf.Repeat(index, pages.Length);
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.PageUp))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index--;
            index = (int)Mathf.Repeat(index, pages.Length);
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.End))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = pages.Length - 1;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 0;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 1;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 2;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 3;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 4;
            pages[index].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            StartCoroutine(ReloadPlayerList());
        }
    }
    public void ReloadPlayer()
    {
        StartCoroutine(ReloadAvatar());
        StartCoroutine(ReloadInfo(input.text));
        StartCoroutine(ReloadPrestige(input.text));
        StartCoroutine(ReloadSkills(input.text));
        StartCoroutine(ReloadItems(input.text));
        //StartCoroutine(ReloadChampionship(input.text));

        // Page 2
        StartCoroutine(equipment.ReloadLists(input.text));
        StartCoroutine(bag.ReloadLists(input.text));
        StartCoroutine(consumable.ReloadLists(input.text));
        StartCoroutine(missionItem.ReloadLists(input.text));
        StartCoroutine(book.ReloadLists(input.text));

        // Page 3
        StartCoroutine(life.ReloadLists(input.text));
        StartCoroutine(adventure.ReloadLists(input.text));
        StartCoroutine(combat.ReloadLists(input.text));
        StartCoroutine(knowledge.ReloadLists(input.text));

        // Page 3
        //StartCoroutine(ReloadStageClash(input.text));

        StartCoroutine(ReloadNPC(input.text));
        StartCoroutine(ReloadTasting(input.text));
        StartCoroutine(ReloadPets(input.text));
        // Page 4
     


        StartCoroutine(stageClash.ReloadRecords(input.text));
        StartCoroutine(royalTradeWar.ReloadRecords(input.text));
        StartCoroutine(minefieldAssault.ReloadRecords(input.text));
        StartCoroutine(wod.ReloadRecords(input.text));
        StartCoroutine(dungeonConquest.ReloadRecords(input.text));
        StartCoroutine(ambushEnigma.ReloadRecords(input.text));
        StartCoroutine(sdol.ReloadRecords(input.text));
        StartCoroutine(throneRivalry.ReloadRecords(input.text));
        StartCoroutine(record.ReloadText(input.text));

    }
    IEnumerator ReloadPlayerList()
    {
        string url = Application.streamingAssetsPath + "/PlayerList.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                
                for (int i = 0; i < contents.Length; i++)
                {
                    string nickname = contents[i].Substring(1);
                    GameObject tab;
                    Cat cat;
                    if (playerList.ContainsKey(nickname))
                        cat = playerList[nickname];
                    else
                    {                      
                        tab = Instantiate(playerTab);
                        tab.transform.SetParent(playerListPanel);
                        tab.transform.SetSiblingIndex(i);
                        cat = new Cat();
                        cat.name = tab.GetComponentInChildren<Text>();
                        cat.name.text = contents[i].Substring(1);
                        cat.avatar = tab.GetComponentsInChildren<Image>()[1];

                        Button btn = tab.GetComponentInChildren<Button>();
                        btn.onClick.AddListener(() =>
                        {
                            input.text = cat.name.text;
                            ReloadPlayer();
                        });
                        playerList.Add(contents[i].Substring(1), cat);
                        StartCoroutine(ReloadCat(cat));
                    }                  
                }
                playerListPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0,273+275.5f*(int)((contents.Length-1)/7));
            }
            else { }
        }
    }
    IEnumerator ReloadCat(Cat cat)
    {
        string url = Application.streamingAssetsPath + "/" + cat.name.text + ".png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
               cat.avatar.sprite = sp;
            }
            else { }
        }
    }
    IEnumerator ReloadAvatar()
    {
        //string url = "https://raw.githubusercontent.com/ilyusha71/TenDaysGame/refs/heads/main/Assets/StreamingAssets/" + input.text + ".png";
        string url = Application.streamingAssetsPath + "/" + input.text + ".png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
                player.sprite = sp;
                cat.gameObject.SetActive(false);
                player.enabled = true;              
            }
            else
            {
                StartCoroutine(ReloadMao());
            }
        }
    }
    IEnumerator ReloadMao()
    {
        string url = Application.streamingAssetsPath + "/qunmao.png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
                player.sprite = sp;
                cat.gameObject.SetActive(false);
                player.enabled = true;
            }
        }
    }
    IEnumerator ReloadInfo(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + ".txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("|");
                //string combine = "";
                //for (int i = 0; i < contents.Length; i++)
                //{
                //    combine += contents[i];
                //}
                info.text = contents[0] + contents[1] + contents[8];
                catbell.text = contents[3];
                casino.text = contents[5];
                judgement.text = contents[7];
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                info.text = "无记录";
                catbell.text = "";
                casino.text = "";
                judgement.text = "";
            }
        }
    }
    IEnumerator ReloadPrestige(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_皇城声望.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                prestige.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                prestige.text = "未取得皇城声望";
            }
        }
    }
    IEnumerator ReloadItems(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_物品.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int i = 0; i < items.Length; i++)
                {
                    if (i < contents.Length)
                        items[i].text = contents[i];
                    else
                        items[i].text = "";
                }
                //print(contents.Length);
                //item.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    items[i].text = "";
                }
                //item.text = "无任何物品";
            }
        }
    }
    IEnumerator ReloadSkills(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_技能.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int i = 0; i < skills.Length; i++)
                {
                    if (i < contents.Length)
                        skills[i].text = contents[i];
                    else
                        skills[i].text = "";
                }
            }
            else
            {
                for (int i = 0; i < skills.Length; i++)
                {
                    skills[i].text = "";
                }
            }
        }
    }
    IEnumerator ReloadChampionship(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_战绩.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                championship.text = webRequest.downloadHandler.text;
            }
            else
            {
                championship.text = "无战绩记录";
            }
        }
    }
    
    IEnumerator ReloadRoyalTradeWar(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_皇家商贸战.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\t");
                for (int i = 0; i < rtwRecords.Length; i++)
                {
                    if (i < contents.Length)
                        rtwRecords[i].text = contents[i];
                    else
                        rtwRecords[i].text = "";
                }
            }
            else
            {
                for (int i = 0; i < rtwRecords.Length; i++)
                {
                    rtwRecords[i].text = "";
                }
            }
        }
    }
    IEnumerator ReloadMinefieldAssault(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_雷区突击.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\t");
                for (int i = 0; i < maRecords.Length; i++)
                {
                    if (i < contents.Length)
                        maRecords[i].text = contents[i];
                    else
                        maRecords[i].text = "";
                }
            }
            else
            {
                for (int i = 0; i < maRecords.Length; i++)
                {
                    maRecords[i].text = "";
                }
            }
        }
    }
    IEnumerator ReloadNPC(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_NPC.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int i = 0; i < npc.Length; i++)
                {
                    if (i < contents.Length)
                        npc[i].text = contents[i];
                    else
                        npc[i].text = "";
                }
            }
            else
            {
                for (int i = 0; i < npc.Length; i++)
                {
                    npc[i].text = "";
                }
            }
        }
    }
    IEnumerator ReloadTasting(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_品鉴.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int i = 0; i < tasting.Length; i++)
                {
                    if (i < contents.Length)
                        tasting[i].text = contents[i];
                    else
                        tasting[i].text = "";
                }
            }
            else
            {
                for (int i = 0; i < tasting.Length; i++)
                {
                    tasting[i].text = "";
                }
            }
        }
    }
    IEnumerator ReloadPets(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_宠物.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                pets.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                pets.text = "";
            }
        }
    }

    [Header("Page 2 Equipment")]
    public ReloadList equipment;
    [Header("Page 2 Bag")]
    public ReloadList bag;
    [Header("Page 2 Consumable")]
    public ReloadList consumable;
    [Header("Page 2 Mission Item")]
    public ReloadList missionItem;
    [Header("Page 2 Book")]
    public ReloadList book;

    [Header("Page 3 Life")]
    public ReloadList life;
    [Header("Page 3 Adventure")]
    public ReloadList adventure;
    [Header("Page 3 Combat")]
    public ReloadList combat;
    [Header("Page 3 Knowledge")]
    public ReloadList knowledge;



    [Header("Stage Clash")]
    public Text[] scRecords;
    public ReloadList stageClash;
    [Header("Royal Trade War")]
    public ReloadList royalTradeWar;
    [Header("Minefield Assault")]
    public ReloadList minefieldAssault;
    [Header("The Wonderland of Desperation")]
    public ReloadList wod;
    [Header("Dungeon Conquest")]
    public ReloadList dungeonConquest;
    [Header("Ambush Enigma")]
    public ReloadList ambushEnigma;
    [Header("Sorcery Duel of Limits")]
    public ReloadList sdol;
    [Header("Throne Rivalry")]
    public ReloadList throneRivalry;
    [Header("Record")]
    public ReloadList record;

    [Serializable]
    public class ReloadList
    {
        public GameObject panel;
        public Text[] list;
        public string title;
        public void Initialize()
        {
            list = panel.GetComponentsInChildren<Text>();
        }
        public IEnumerator ReloadText(string nick)
        {
            string url = Application.streamingAssetsPath + "/" + nick + "_" + title + ".txt";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                if (string.IsNullOrEmpty(webRequest.error))
                {
                    list[0].text = webRequest.downloadHandler.text;
                }
                else
                {
                    list[0].text = "无记录";
                }
            }
        }
        public IEnumerator ReloadLists(string nick)
        {
            string url = Application.streamingAssetsPath + "/" + nick + "_"+ title+".txt";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                if (string.IsNullOrEmpty(webRequest.error))
                {
                    string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (i < contents.Length) list[i].text = contents[i];
                        else list[i].text = "";
                    }
                }
                else { for (int i = 0; i < list.Length; i++) { list[i].text = ""; } }
            }
        }
        public IEnumerator ReloadRecords(string nick)
        {
            string url = Application.streamingAssetsPath + "/" + nick + "_" + title + ".txt";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                if (string.IsNullOrEmpty(webRequest.error))
                {
                    string[] contents = webRequest.downloadHandler.text.Split("\t");
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (i < contents.Length) list[i].text = contents[i];
                        else list[i].text = "";
                    }
                }
                else { for (int i = 0; i < list.Length; i++) { list[i].text = ""; } }
            }
        }
    }
}
