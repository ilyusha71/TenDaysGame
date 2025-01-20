using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RearrangementController : MonoBehaviour
{
    public Transform[] slots;
    protected Vector3[] pos;
    protected List<int> lotteryPool = new();
    public bool shuffle = false;
    private void Awake() => Initialize();
    protected virtual void Initialize()
    {
        pos = new Vector3[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            pos[i] = slots[i].position;
            lotteryPool.Add(i);
        }
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            ReturnSlots();
        if (Input.GetKeyDown(KeyCode.R))
            RandomlyAllocate();
        if (Input.GetKeyDown(KeyCode.F5) && shuffle)
            Shuffle(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
    }
    public virtual void ReturnSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].position = pos[i];
            slots[i].SetSiblingIndex(i);
        }
    }
    public void RandomlyAllocate()
    {
        var rand = slots.OrderBy(t => Random.Range(0, slots.Length)).ToList();
        for (int i = 0; i < rand.Count; i++)
        {
            rand[i].position = pos[i];
            rand[i].SetSiblingIndex(i);
        }

        //List<int> rand = lotteryPool.ToList();
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    int index = Random.Range(0, rand.Count);
        //    print(rand[index]);
        //    slots[i].position = pos[rand[index]];
        //    slots[i].SetSiblingIndex(rand[index]);
        //    rand.Remove(rand[index]);
        //}
    }
    public void Shuffle(Vector3 v)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].position = v;
            slots[i].SetSiblingIndex(Random.Range(0, slots.Length));
        }
    }
    public Vector3 GetPosition(int index) { return pos[index]; }
}
