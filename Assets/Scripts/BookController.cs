using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    public GameObject book;
    public GameObject[] pages;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
            book.SetActive(!book.activeSelf);
        if (book.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                Flip();
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                Flop();

        }
    }
    public void Flip()
    {
        pages[index].gameObject.SetActive(false);
        index++;
        index = (int)Mathf.Repeat(index, pages.Length);
        pages[index].gameObject.SetActive(true);
    }
    public void Flop()
    {
        pages[index].gameObject.SetActive(false);
        index--;
        index = (int)Mathf.Repeat(index, pages.Length);
        pages[index].gameObject.SetActive(true);
    }
}
