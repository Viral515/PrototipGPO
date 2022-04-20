using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
}

//позволяет получить доступ ко всем пунктам этого скрипта из любой части программы
[System.Serializable]

public class Item
{
    public int id;
    public string name;
    public Sprite image;
}
