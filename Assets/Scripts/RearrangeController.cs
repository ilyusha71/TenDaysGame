using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RearrangeController : MonoBehaviour
{
    public Transform[] slots;
    public List<Vector3> lotteryPool = new List<Vector3>();
    public Vector3[] pos;
    private void Awake()
    {
        pos = new Vector3[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            pos[i] = slots[i].position;
            lotteryPool.Add(pos[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            ReturnSlots();
        if (Input.GetKeyDown(KeyCode.R))
            RandomlyAllocate();
    }
    void ReturnSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].position = pos[i];
        }
    }
    void RandomlyAllocate()
    {
        List<Vector3> rand = lotteryPool.ToList();
        for (int i = 0; i < slots.Length; i++)
        {
            int index = Random.Range(0, rand.Count);
            slots[i].position = rand[index];
            rand.Remove(rand[index]);
        }
    }
}
