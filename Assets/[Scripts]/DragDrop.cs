using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : 
MonoBehaviour, IPointerDownHandler, IBeginDragHandler, 
IDragHandler, IEndDragHandler//, IDropHandler
{
    private Canvas canvas;
    MatchObj thisMatch;
    RectTransform imgObjRect;
    CanvasGroup canvasGroup;

    void Start()
    {
        thisMatch = GetComponentInParent<MatchObj>();
        imgObjRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
        Debug.Log(thisMatch.slotIndex.x + ", " + thisMatch.slotIndex.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        imgObjRect.parent = canvas.GetComponent<RectTransform>();
        canvasGroup.blocksRaycasts = false;
        Debug.Log(eventData.pointerDrag.name + " " + 
        thisMatch.slotIndex.x + ", " + thisMatch.slotIndex.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // object moves with mouse drag and sticks to mouse independant of canvas scale
        imgObjRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On End Drag");
        canvasGroup.blocksRaycasts = true;
        Debug.Log(eventData.pointerDrag.name + " " + 
        thisMatch.slotIndex.x + ", " + thisMatch.slotIndex.y);

        MatchObj newMatch = eventData.pointerDrag.GetComponentInParent<MatchObj>();
        
        Debug.Log(eventData.pointerDrag.name + " " + 
        newMatch.slotIndex.x + ", " + newMatch.slotIndex.y);
    }
}
