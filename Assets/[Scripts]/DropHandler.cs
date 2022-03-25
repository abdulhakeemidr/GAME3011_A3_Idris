using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PERIPHERALVEC
{
    public static Vector2Int TOP = new Vector2Int(0, -1);
    public static Vector2Int BOTTOM = new Vector2Int(0, 1);
    public static Vector2Int LEFT = new Vector2Int(-1, 0);
    public static Vector2Int RIGHT = new Vector2Int(1, 0);
}

public class DropHandler : MonoBehaviour, IDropHandler
{
    Vector2Int a = PERIPHERALVEC.BOTTOM;
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

            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(this.transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
