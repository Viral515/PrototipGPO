using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Image buttonImage;

    public Sprite buttonDown;
    public Sprite buttonUp;

    public GameObject textMove;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = buttonDown;
        textMove.gameObject.transform.localPosition = new Vector3Int(0, -6, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = buttonUp;
        textMove.gameObject.transform.localPosition = new Vector3Int(0, 6, 0);
    }
}
