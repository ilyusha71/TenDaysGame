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
    Dictionary<string, Cat> playerList = new Dictionary<string, Cat>();
    //string[] playerList;
    [Header("Cat")]
    public GameObject cat;
    public Image player;
    public InputField input;
    public GameObject[] pages;
    int index;
    [Header("Info")]
    public Text catbell;
    public Text casino;
    public Text judgement;
    public Text txtFaction;
    public Text txtPlayer;
    public Text txtBirthday;
    public Text info;
    public Text prestige;
    public Text championship;
    public Text pets;
    public Text carriers;
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
        //items = itemPanel.GetComponentsInChildren<Text>();
        //skills = skillPanel.GetComponentsInChildren<Text>();
        npc = npcPanel.GetComponentsInChildren<Text>();
        tasting = tastingPanel.GetComponentsInChildren<Text>();
        equipment.Initialize();
        bag.Initialize();
        consumable.Initialize();
        missionItem.Initialize();
        book.Initialize();

        talent.Initialize();
        life.Initialize();
        knowledge.Initialize();
        spell.Initialize();

        adventure.Initialize();
        skill.Initialize();
        shooting.Initialize();
        magic.Initialize();
        licence.Initialize();

        //combat.Initialize();
     
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.PageDown))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index++;
            index = (int)Mathf.Repeat(index, pages.Length);
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.PageUp))
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
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 5;
            pages[index].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F12))
            StartCoroutine(ReloadPlayerList());
    }
    public void LoadPlayerList()
    {
        StartCoroutine(ReloadPlayerList());
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
                    string nickname = contents[i].Split("\t")[0];
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
                        cat.name.text = nickname;
                        cat.avatar = tab.GetComponentsInChildren<Image>()[1];

                        Button btn = tab.GetComponentInChildren<Button>();
                        btn.onClick.AddListener(() =>
                        {
                            input.text = cat.name.text;
                            ReloadPlayer();
                        });
                        playerList.Add(nickname, cat);
                        StartCoroutine(ReloadCat(cat));
                    }
                }
                playerListPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 273 + 275.5f * (int)((contents.Length - 1) / 7));
            }
            else { }
        }
    }
    public void ReloadPlayer()
    {
        StartCoroutine(ReloadAvatar());
        StartCoroutine(ReloadInfo(input.text));
        StartCoroutine(ReloadPrestige(input.text));
        StartCoroutine(ReloadNPC(input.text));
        StartCoroutine(ReloadTasting(input.text));
        StartCoroutine(ReloadPets(input.text));
        StartCoroutine(ReloadCarriers(input.text));
        //StartCoroutine(ReloadSkills(input.text));
        //StartCoroutine(ReloadItems(input.text));
        //StartCoroutine(ReloadChampionship(input.text));

        // Page 2
        StartCoroutine(equipment.ReloadLists(input.text));
        StartCoroutine(bag.ReloadLists(input.text));
        StartCoroutine(consumable.ReloadLists(input.text));
        StartCoroutine(missionItem.ReloadLists(input.text));
        StartCoroutine(book.ReloadLists(input.text));

        // Page 3
        StartCoroutine(talent.ReloadLists(input.text));
        StartCoroutine(life.ReloadLists(input.text));
          StartCoroutine(knowledge.ReloadLists(input.text));
        StartCoroutine(spell.ReloadLists(input.text));
        //StartCoroutine(combat.ReloadLists(input.text));

        // Page 5
        StartCoroutine(adventure.ReloadLists(input.text));
        StartCoroutine(skill.ReloadLists(input.text));
        StartCoroutine(shooting.ReloadLists(input.text));
        StartCoroutine(magic.ReloadLists(input.text));
        StartCoroutine(licence.ReloadLists(input.text));
        
        StartCoroutine(record.ReloadText(input.text));
    }
    void UpdateRank(string playerName) 
    {
        StartCoroutine(stageClash.ReloadRank(playerName)); //驿站风云
        StartCoroutine(royalTradeWar.ReloadRank(playerName)); //皇家商贸战
        StartCoroutine(minefieldAssault.ReloadRank(playerName)); //雷区突击
        StartCoroutine(wod.ReloadRank(playerName)); //绝望梦境
        StartCoroutine(dungeonConquest.ReloadRank(playerName)); //地城争夺
        StartCoroutine(ambushEnigma.ReloadRank(playerName)); //伏击迷局
        StartCoroutine(liarsBar.ReloadRank(playerName)); //老猫酒馆
        StartCoroutine(catBingo.ReloadRank(playerName)); //猫猫宾果
        StartCoroutine(sdol.ReloadRank(playerName)); //极限斗法
        StartCoroutine(throneRivalry.ReloadRank(playerName)); //王权纷争
        StartCoroutine(bicolorRacing.ReloadRank(playerName)); //双色斗猫
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
        catbell.text = "";
        casino.text = "";
        judgement.text = "";
        txtFaction.text = "";
        txtPlayer.text = "";
        txtBirthday.text = "";
        string url = Application.streamingAssetsPath + "/" + nick + ".txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                info.text = webRequest.downloadHandler.text.Split("\r\n")[5];
                StartCoroutine(ReloadMain(nick));
                UpdateRank(webRequest.downloadHandler.text.Split("|")[1]);
            }
            else
            {
                info.text = "无记录";
                UpdateRank(nick);
            }
        }
    }
    public IEnumerator ReloadMain(string playerName)
    {
        //yield return new WaitForSecondsRealtime(0.1f);
        string url = Application.streamingAssetsPath + "/PlayerList.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int order = 0; order < contents.Length; order++)
                {
                    if (contents[order].Contains(playerName))
                    {
                        string[] data = contents[order].Split("\t");
                        catbell.text = data[3];
                        int vip = int.Parse(data[5]);
                        casino.text = data[4] + (vip > 0 ? ("(VIP" + data[5] + ")") : "");
                        judgement.text = data[6];
                        txtFaction.text = data[2];
                        txtPlayer.text = data[1];
                        txtBirthday.text = data[7];
                    }
                }
            }
            //else { for (int i = 0; i < list.Length; i++) { list[i].text = ""; } }
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
    IEnumerator ReloadCarriers(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_载具.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                carriers.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                carriers.text = "";
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

    [Header("Page 3 Talent")]
    public ReloadList talent;
    [Header("Page 3 Life")]
    public ReloadList life;
    [Header("Page 3 Knowledge")]
    public ReloadList knowledge;
    [Header("Page 3 Spell")]
    public ReloadList spell;
    [Header("Page 3 Combat")]
    public ReloadList combat;


    [Header("Page 5 Adventure")]
    public ReloadList adventure;
    [Header("Page 5 Skill")]
    public ReloadList skill;
    [Header("Page 5 Shooting")]
    public ReloadList shooting;
    [Header("Page 5 Magic")]
    public ReloadList magic;
    [Header("Page 5 License")]
    public ReloadList licence;



    [Header("<color=green>驿站风云 Stage Clash")] //驿站风云
    public ReloadList stageClash;
    [Header("<color=green>皇家商贸战 Royal Trade War</color>")] //皇家商贸战
    public ReloadList royalTradeWar;
    [Header("<color=green>雷区突击 Minefield Assault</color>")] //雷区突击
    public ReloadList minefieldAssault;
    [Header("<color=green>绝望梦境 The Wonderland of Desperation</color>")] //绝望梦境
    public ReloadList wod;
    [Header("<color=green>地城争夺 Dungeon Conquest</color>")] //地城争夺
    public ReloadList dungeonConquest;
    [Header("<color=green>伏击迷局 Ambush Enigma</color>")] //伏击迷局
    public ReloadList ambushEnigma;
    [Header("<color=green>老猫酒馆 Liar's Bar</color>")] //老猫酒馆
    public ReloadList liarsBar;
    [Header("<color=green>猫猫宾果 Cat Bingo</color>")] //猫猫宾果
    public ReloadList catBingo;
    [Header("<color=green>极限斗法 Sorcery Duel of Limits</color>")] //极限斗法
    public ReloadList sdol;
    [Header("<color=green>王权纷争 Throne Rivalry</color>")] //王权纷争
    public ReloadList throneRivalry;
    [Header("<color=green>双色斗猫 Bicolor Racing</color>")] //双色斗猫
    public ReloadList bicolorRacing;
    [Header("Record")]
    public ReloadList record;


    [Serializable]
    public class ReloadList
    {
        public string title;
        public GameObject panel;
        public Text[] list;       
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
        public IEnumerator ReloadRank(string playerName)
        {
            for (int i = 0; i < list.Length; i++) { list[i].text = ""; }
            //yield return new WaitForSecondsRealtime(0.1f);
            string url = Application.streamingAssetsPath + "/Game_" + title + ".txt";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                if (string.IsNullOrEmpty(webRequest.error))
                {
                    string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                    for (int data = 0; data < contents.Length; data++)
                    {
                        string[] records = contents[data].Split("\t");
                        if (records[0]==playerName)
                        {                            
                            for (int i = 0; i < list.Length; i++)
                            {
                                list[i].text = records[i + 2];
                            }
                        }
                    }
                }
            }
        }
    }
}
