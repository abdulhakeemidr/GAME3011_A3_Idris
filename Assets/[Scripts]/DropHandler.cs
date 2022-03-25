using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    MatchObj thisObj;
    void Start()
    {
        thisObj = GetComponent<MatchObj>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
        // pointerDrag is the GameObject being dragged by mouse
        if(eventData.pointerDrag != null)
        {
            Debug.Log(eventData.pointerDrag.name + " " + 
            thisObj.slotIndex.x + ", " + thisObj.slotIndex.y);
            eventData.pointerDrag.GetComponent<RectTransform>().parent = this.transform;
            
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
