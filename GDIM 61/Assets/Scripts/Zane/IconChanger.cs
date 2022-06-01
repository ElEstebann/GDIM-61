using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Written by Zane
public class IconChanger : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] private GameObject iconOne;
    [SerializeField] private GameObject iconTwo;
    [SerializeField] private GameObject iconThree;
    [SerializeField] private GameObject iconFour;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // enables the corresponding icon when hovered over a button
        iconOne.SetActive(true);
        iconTwo.SetActive(false);
        iconThree.SetActive(false);
        iconFour.SetActive(false);
        Debug.Log("Mouse enter");
    }

    public void OnSelect(BaseEventData eventData)
    {
        // enables the corresponding icon when button is selected
        iconOne.SetActive(true);
        iconTwo.SetActive(false);
        iconThree.SetActive(false);
        iconFour.SetActive(false);
        Debug.Log("Button Selected");
    }
}
