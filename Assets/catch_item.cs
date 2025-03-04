using UnityEngine;
using UnityEngine.EventSystems;

public class catch_item : MonoBehaviour, IDropHandler
{
    private drag_drop drag_drop_script;

    private void Awake()
    {
        drag_drop_script = GameObject.Find("Apple").GetComponent<drag_drop>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) //checking if we're currently dragging anything
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; //snapping the apple to the frame/shelf position
            drag_drop_script.catch_object();
        }
    }
}
