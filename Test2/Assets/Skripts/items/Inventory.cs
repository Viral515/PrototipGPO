using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<ItemInventory> items = new List<ItemInventory>();
    public int maxCount;
    private int maxCountOfStack = 128;
    public int currentID;
    private bool pauseGame = false;
    public GameObject gameObjectShow;
    public GameObject backGround;
    public GameObject dialogueSpace;
    public GameObject inventoryMainObject;
    public DataBase data;
    public Camera cam;
    public EventSystem eventSystem;
    public ItemInventory currentItem;
    public RectTransform movingObject;
    public Vector3 offset;

    public void Start()
    {
        if (items.Count == 0)
        {
            AddGraphics();
        }

        //цикл заполнения инвентаря для тестов
        for (int i = 0; i < maxCount; i++)
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, 99));
        }
        UpdateInventory();
    }

    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }

        //открытие/закрытие инвентаря
        if (Input.GetKeyDown(KeyCode.Z) && (dialogueSpace.activeSelf == false))
        {
            if (pauseGame == false)
            {
                Time.timeScale = 0.0f;
                pauseGame = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                pauseGame = false;
            }
            backGround.SetActive(!backGround.activeSelf);
            if (backGround.activeSelf)
            {
                UpdateInventory();
            }
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if(items[i].id == item.id)
            {
                if (items[0].count < maxCountOfStack)
                {
                    items[i].count += count;
                    if (items[i].count > maxCountOfStack)
                    {
                        count = items[i].count - maxCountOfStack;
                        items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }

        if (count > 0)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

    //добавление слота в панель инвентаря
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
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    //добавление предмета в один из слотов инвентаря
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
            GameObject newItem = Instantiate(gameObjectShow, inventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory itemInventory = new ItemInventory();
            itemInventory.itemGameObject = newItem;

            //положение объекта в пространстве (коорды, размеры, поворот)
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button temporaryButton = newItem.GetComponent<Button>();

            //???
            temporaryButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(itemInventory);
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if ((items[i].id != 0) && (items[i].count > 1))
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObject.GetComponentInChildren<Image>().sprite = data.items[items[i].id].image;
        }
    }

    public void SelectObject()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(eventSystem.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].image;

            AddItem(currentID, data.items[0], 0);
        }
        else
        {
            ItemInventory itemInv = items[int.Parse(eventSystem.currentSelectedGameObject.name)];

            if (currentItem.id != itemInv.id)
            {
                AddInventoryItem(currentID, items[int.Parse(eventSystem.currentSelectedGameObject.name)]);
                AddInventoryItem(int.Parse(eventSystem.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if (itemInv.count + currentItem.count <= maxCountOfStack)
                {
                    itemInv.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[itemInv.id], itemInv.count + currentItem.count - maxCountOfStack);
                    itemInv.count = maxCountOfStack;
                }

                itemInv.itemGameObject.GetComponentInChildren<Text>().text = itemInv.count.ToString();
            }

            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = inventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public ItemInventory CopyInventoryItem(ItemInventory oldItem)
    {
        ItemInventory newItem = new ItemInventory();
        newItem.id = oldItem.id;
        newItem.itemGameObject = oldItem.itemGameObject;
        newItem.count = oldItem.count;

        return newItem;
    }
}

//даёт доступ к компонентам скрипта из любого места в проекте
[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObject;
    public int count;
}
