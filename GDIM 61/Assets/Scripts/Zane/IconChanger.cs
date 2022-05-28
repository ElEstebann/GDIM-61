using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconChanger : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] private GameObject iconOne;
    [SerializeField] private GameObject iconTwo;
    [SerializeField] private GameObject iconThree;
    [SerializeField] private GameObject iconFour;

    public void OnPointerEnter(PointerEventData eventData)
    {
        iconOne.SetActive(true);
        iconTwo.SetActive(false);
        iconThree.SetActive(false);
        iconFour.SetActive(false);
        Debug.Log("Mouse enter");
    }

    public void OnSelect(BaseEventData eventData)
    {
        iconOne.SetActive(true);
        iconTwo.SetActive(false);
        iconThree.SetActive(false);
        iconFour.SetActive(false);
        Debug.Log("Button Selected");
    }
}
