using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadPlayerAvatar : MonoBehaviour
{
    public GameObject[] cat;
    public Image[] player;
    public InputField[] input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            Cat();
        if (Input.GetKeyDown(KeyCode.Keypad1))
            StartCoroutine(Reload(1));
        if (Input.GetKeyDown(KeyCode.Keypad2))
            StartCoroutine(Reload(2));
        if (Input.GetKeyDown(KeyCode.Keypad4))
            StartCoroutine(ReloadByName(1));
        if (Input.GetKeyDown(KeyCode.Keypad5))
            StartCoroutine(ReloadByName(2));

    }
    public void Cat()
    {
        for (int i = 0; i < cat.Length; i++)
        {
            cat[i].gameObject.SetActive(true);
            player[i].enabled = false;
        }
    }
    //public void Reload(int i)
    //{
    //    cat[i-1].gameObject.SetActive(false);
    //    Texture2D img = Resources.Load<Texture2D>("P" + i.ToString());
    //    Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
    //    player[i - 1].sprite = sp;
    //    player[i - 1].enabled = true;
    //}
    IEnumerator Reload(int i)
    {

        string url = Application.streamingAssetsPath + "/P" + i.ToString() + ".png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
                player[i - 1].sprite = sp;
                cat[i - 1].gameObject.SetActive(false);
                player[i - 1].enabled = true;
            }
        }
    }
    public void ReloadAvatar(int i)
    {
        StartCoroutine(ReloadByName(i));
    }
    IEnumerator ReloadByName(int i)
    {
        string url = Application.streamingAssetsPath + "/" + input[i-1].text + ".png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
                player[i - 1].sprite = sp;
                cat[i - 1].gameObject.SetActive(false);
                player[i - 1].enabled = true;
            }
            else
            {
                StartCoroutine(ReloadMao(i));
            }
        }
    }
    IEnumerator ReloadMao(int i)
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
                player[i - 1].sprite = sp;
                cat[i - 1].gameObject.SetActive(false);
                player[i - 1].enabled = true;
            }          
        }
    }
}
