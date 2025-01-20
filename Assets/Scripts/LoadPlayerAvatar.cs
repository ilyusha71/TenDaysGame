using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadPlayerAvatar : MonoBehaviour
{
    public Image[] player;
    public InputField[] input;
    Sprite[] avatar;
    public static LoadPlayerAvatar Instance { get; private set; }
    private void Awake() => Instance = this;
    // Start is called before the first frame update
    void Start()
    {
        avatar = new Sprite[player.Length];
        for (int i = 0; i < avatar.Length; i++)
        {
            avatar[i]=player[i].sprite;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
            ResetAvatar();
    }
    public void ResetAvatar()
    {
        for (int i = 0; i < avatar.Length; i++)
        { 
            player[i].sprite = avatar[i];
        }
    }
    public void ChangeAvatar(int index, Sprite sp) 
    {
        player[index].sprite = sp;
    }
    public void LoadAvatar(int index)
    {
        StartCoroutine(ReloadByName(index));
    }
    IEnumerator ReloadByName(int index)
    {
        string url = Application.streamingAssetsPath + "/" + input[index].text + ".png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
                player[index].sprite = sp;
            }
            else
            {
                StartCoroutine(ReloadMao(index));
            }
        }
    }
    IEnumerator ReloadMao(int index)
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
                player[index].sprite = sp;
            }          
        }
    }
}
