using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadAvatar : MonoBehaviour
{
    public Image player;
    public InputField input;
    Sprite avatar;
    // Start is called before the first frame update
    void Start()
    {
        avatar = player.sprite;
    }
    public void Load()
    {
        StartCoroutine(ReloadByName());
    }
    IEnumerator ReloadByName()
    {
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
            }
            else player.sprite = avatar;
        }
    }
}
