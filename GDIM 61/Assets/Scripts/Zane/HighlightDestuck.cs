using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class HighlightDestuck : EventTrigger
{

    private Selectable selectable;

    public void Awake()
    {
        selectable = GetComponent<Selectable>();
    }

    public void Destuck()
    {
        EventSystem e = EventSystem.current;
        if (selectable.interactable && e.currentSelectedGameObject != gameObject)
        {
            //Somebody else is still selected?!? Screw that. Select us now.
            e.SetSelectedGameObject(gameObject);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Destuck();
    }
}