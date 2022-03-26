using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This script is ATTACHED to the Sprite Image GameObject (child of MatchObj GameObject)
public class DragDrop : MonoBehaviour, 
IPointerDownHandler, IBeginDragHandler, 
IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    [SerializeField]
    public MatchObj originalMatch;
    [SerializeField]
    public MatchObj newMatch;
    RectTransform imgObjRect;
    CanvasGroup canvasGroup;

    void Start()
    {
        originalMatch = GetComponentInParent<MatchObj>();
        imgObjRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
        Debug.Log(originalMatch.slotIndex.x + ", " + originalMatch.slotIndex.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        imgObjRect.SetParent(canvas.GetComponent<RectTransform>());
        canvasGroup.blocksRaycasts = false;
        Debug.Log(eventData.pointerDrag.name + " " + 
        originalMatch.slotIndex.x + ", " + originalMatch.slotIndex.y);
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
        originalMatch.slotIndex.x + ", " + originalMatch.slotIndex.y);
        
        // gets the reference to the new MatchObj the image was placed at
        newMatch = eventData.pointerDrag.GetComponentInParent<MatchObj>();

        if(newMatch == null)
        {
            ResetImageMatchPos();
            return;
        }

        if(newMatch.slotIndex != (originalMatch.slotIndex + PERIPHERALVEC.LEFT)
        && newMatch.slotIndex != (originalMatch.slotIndex + PERIPHERALVEC.RIGHT)
        && newMatch.slotIndex != (originalMatch.slotIndex + PERIPHERALVEC.TOP)
        && newMatch.slotIndex != (originalMatch.slotIndex + PERIPHERALVEC.BOTTOM))
        {
            ResetImageMatchPos();
            return;
        }

        var origMatchRect = originalMatch.gameObject.GetComponent<RectTransform>();
        // the RectTransform of original Image GameObject that is a child of the new MatchObj GameObject
        RectTransform newMatchImgOrig = newMatch.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        // attaches the original Img GameObject (child) of the new MatchObj 
        // to the original MatchObj (match Obj first clicked)
        newMatchImgOrig.SetParent(origMatchRect);
        newMatchImgOrig.LeanMove(Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutCubic);
        SwapImgRefData(ref originalMatch.sprite, ref newMatch.sprite);
        
        // original Image GameObject of the new MatchObj gameObject is redefined
        // so the original MatchObject is the MatchObj that is first moved
        newMatchImgOrig.GetComponent<DragDrop>().originalMatch = 
        newMatchImgOrig.GetComponentInParent<MatchObj>();
        
        GridOrganizer.instance.onReset.GatherSwappedMatches(originalMatch, newMatch);
        // reset original match as the new match
        originalMatch = newMatch;

        GridOrganizer.instance.onReset.FindMatchOnMoved();
    }

    void ResetImageMatchPos()
    {
        imgObjRect.SetParent(originalMatch.gameObject.GetComponent<RectTransform>());
        imgObjRect.anchoredPosition = Vector2.zero;
    }

    void SwapImgRefData(ref Sprite original, ref Sprite newMatch)
    {
        Sprite temp = newMatch;

        newMatch = original;
        original = temp;
    }
}

/* if(newMatch.slotIndex == (originalMatch.slotIndex + PERIPHERALVEC.LEFT))
        {
            //ResetImageMatchPos();
            Debug.Log("Match");
            Debug.Log(eventData.pointerDrag.name + " " + 
            newMatch.slotIndex.x + ", " + newMatch.slotIndex.y);
            return;
        }
        else if (newMatch.slotIndex == (originalMatch.slotIndex + PERIPHERALVEC.RIGHT))
        {
            Debug.Log("Match");
            Debug.Log(eventData.pointerDrag.name + " " + 
            newMatch.slotIndex.x + ", " + newMatch.slotIndex.y);
            return;
        }
        else if(newMatch.slotIndex == (originalMatch.slotIndex + PERIPHERALVEC.TOP))
        {
            Debug.Log("Match");
            Debug.Log(eventData.pointerDrag.name + " " + 
            newMatch.slotIndex.x + ", " + newMatch.slotIndex.y);
            return;
        }
        else if(newMatch.slotIndex == (originalMatch.slotIndex + PERIPHERALVEC.BOTTOM))
        {
            Debug.Log("Match");
            Debug.Log(eventData.pointerDrag.name + " " + 
            newMatch.slotIndex.x + ", " + newMatch.slotIndex.y);
            return;
        }
        else
        {
            ResetImageMatchPos();
            return;
        } */