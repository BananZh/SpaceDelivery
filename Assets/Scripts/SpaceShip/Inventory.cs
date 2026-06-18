using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Slot> slots;
    public int slotsQuanity;

    void Start()
    {
        slots = new List<Slot>();
        for (int i = 0; i < slotsQuanity; i++)
        {
            slots.Add(new Slot(null, 0));
        }
    }

    public void AddItemIntoInventorySlot(GameObject obj, int q)
    {
        Slot s = FindInventorySlot(obj.name);
        if (s == null) return;
        s.itemName = obj.name; s.quantity += q; s.isEmpty = false;
    }

    Slot FindInventorySlot(string objName)
    {
        print(slots);
        foreach (Slot s in slots)
        {
            if (s.isEmpty || s.itemName == objName)
            {
                return s;
            }
        }
        print("Подходящего слота не найдено!");
        return null;
    }
}

[System.Serializable]
public class Slot
{
    public string itemName;
    public int quantity;
    public bool isEmpty = true;

    public Slot(GameObject obj, int q)
    {
        itemName = obj.name;
        if (itemName != null) { isEmpty = false; return; }
        quantity = q;
    }
}