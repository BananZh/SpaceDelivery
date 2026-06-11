using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Без [System.Serializable] у класса Weapon этот список 
    // ВООБЩЕ НЕ ПОЯВИТСЯ в инспекторе Unity!
    public List<Slot> slots;
}

[System.Serializable] // <- Этот атрибут делает магию
public class Slot
{
    public GameObject item;
    public int quantity;
}