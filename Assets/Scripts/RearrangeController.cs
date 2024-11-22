using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RearrangeController : MonoBehaviour
{
    public Transform[] slots;
    public List<int> lotteryPool = new List<int>();
    public Vector3[] pos;
    public bool shuffle=false;

    private void Awake()
    {
        pos = new Vector3[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            pos[i] = slots[i].position;
            lotteryPool.Add(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            ReturnSlots();
        if (Input.GetKeyDown(KeyCode.R))
            RandomlyAllocate();
        if (Input.GetKeyDown(KeyCode.F5)&&shuffle)
            Shuffle(new Vector3(Screen.width*0.5f,Screen.height*0.5f,0));
    }
    void ReturnSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].position = pos[i];
        }
    }
    public void RandomlyAllocate()
    {
        List<int> rand = lotteryPool.ToList();
        for (int i = 0; i < slots.Length; i++)
        {
            int index = Random.Range(0, rand.Count);
            slots[i].position = pos[rand[index]];
            slots[i].SetSiblingIndex(rand[index]);
            rand.Remove(rand[index]);
        }
    }

    public void Shuffle(Vector3 v)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].position = v;
            slots[i].SetSiblingIndex(Random.Range(0, slots.Length));
        }
    }
}
