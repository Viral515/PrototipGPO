using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<ItemInventory> items = new List<ItemInventory>();
    public int maxCount;
    public GameObject gameObjectShow;
    public DataBase data;

    public GameObject inventoryMainObject;

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.image;

        if ((count > 1) && (item.id != 0))
        {
            //преобразует кол-во предметов в текст
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
    }

    public void AddInventoryItem(int id, ItemInventory inventoryItem)
    {
        items[id].id = inventoryItem.id;
        items[id].count = inventoryItem.count;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.items[inventoryItem.id].image;

        if ((inventoryItem.count > 1) && (inventoryItem.id != 0))
        {
            //преобразует кол-во предметов в текст
            items[id].itemGameObject.GetComponentInChildren<Text>().text = inventoryItem.count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            //для перемещения объекта "новый предмет". 
            //Про Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
            GameObject newItem = Instantiate(gameObjectShow, inventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory itemInventory = new ItemInventory();
            itemInventory.itemGameObject = newItem;

            //положение объекта в пространстве (коорды, размеры, поворот)
            //подробнее: https://docs.unity3d.com/ScriptReference/RectTransform.html
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button temporaryButton = newItem.GetComponent<Button>();

            items.Add(itemInventory);
        }
    }
}

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObject;
    public int count;
}
